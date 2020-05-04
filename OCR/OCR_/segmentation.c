#include <stdlib.h>
#include <stdio.h>
#include <SDL2/SDL.h>
#include <SDL2/SDL_image.h>
#include <err.h>

#include "segmentation.h"
#include "grayscale.h"
#include "thresholding.h"
#include "nn/network.h"

char* imagePath;

size_t CHAR_WIDTH;
size_t CHAR_HEIGHT;

char* rendered_text = "";
char* EXPECTED_TEXT = ""; // Change this only when training
char* training_file = "";

//-----------------------------------------------------------------------------
// TOOLBOX

void draw_hline(SDL_Surface* img, size_t x, size_t y)
{
    for (size_t i = 0; i < x; i++)
    {
        Uint32 pixel = get_pixel(img, i, y);
        pixel = SDL_MapRGB(img->format, 255, 0, 0); // Set red
        put_pixel(img, i, y, pixel);
    }
}

void draw_vline_red(SDL_Surface* img, size_t x, size_t yMin, size_t yMax)
{
    for (size_t y = yMin; y < yMax; y++)
    {
        Uint32 pixel = get_pixel(img, x, y);
        pixel = SDL_MapRGB(img->format, 255, 0, 0);
        put_pixel(img, x, y, pixel);
    }
}

void draw_vline_blue(SDL_Surface* img, size_t x, size_t yMin, size_t yMax)
{
    for (size_t y = yMin; y < yMax; y++)
    {
        Uint32 pixel = get_pixel(img, x, y);
        pixel = SDL_MapRGB(img->format, 0, 0, 255);
        put_pixel(img, x, y, pixel);
    }
}

void draw_vline_cyan(SDL_Surface* img, size_t x, size_t yMin, size_t yMax)
{
    for (size_t y = yMin; y < yMax; y++)
    {
        Uint32 pixel = get_pixel(img, x, y);
        pixel = SDL_MapRGB(img->format, 0, 255, 255);
        put_pixel(img, x, y, pixel);
    }
}

void draw_vline_green(SDL_Surface* img, size_t x, size_t yMin, size_t yMax)
{
    for (size_t y = yMin; y < yMax; y++)
    {
        Uint32 pixel = get_pixel(img, x, y);
        pixel = SDL_MapRGB(img->format, 0, 255, 0);
        put_pixel(img, x, y, pixel);
    }
}

void draw_vline_yellow(SDL_Surface* img, size_t x, size_t yMin, size_t yMax)
{
    for (size_t y = yMin; y < yMax; y++)
    {
        Uint32 pixel = get_pixel(img, x, y);
        pixel = SDL_MapRGB(img->format, 255, 255, 0);
        put_pixel(img, x, y, pixel);
    }
}

void refresh_window(SDL_Surface* image)
{
    SDL_DestroyTexture(texture);
    SDL_DestroyRenderer(renderer);
    renderer = SDL_CreateRenderer(screen, -1, 0);
    texture = SDL_CreateTextureFromSurface(renderer, image);
    render();
}

double* build_char(SDL_Surface* img, size_t x, size_t y, size_t width, size_t height)
{
    // Init char
    double* matrix = malloc(sizeof(double) * CHAR_WIDTH * CHAR_HEIGHT);
    for (size_t i = 0; i < CHAR_WIDTH * CHAR_HEIGHT; i++)
        matrix[i] = 0.0f;

    double pixWidth = (double)width / CHAR_WIDTH;
    double pixHeight = (double)height / CHAR_HEIGHT;

    // For each pixel in the output
    for (size_t i = 0; i < CHAR_WIDTH; i++)
    {
        for (size_t j = 0; j < CHAR_HEIGHT; j++)
        {
            double color = 0;
            double nbPix = 0;

            for (size_t posX = floor((double)i * pixWidth); 
                posX < floor((i + 1.f) * pixWidth); posX++)
            {
                for (size_t posY = floor((double)j * pixHeight); 
                    posY < floor((j + 1.f) * pixHeight); posY++)
                {
                    Uint32 pixel = get_pixel(img, x - posX, y + posY);
                    Uint8 r, g, b;
                    SDL_GetRGB(pixel, img->format, &r, &g, &b);

                    // Get color for unique pixel
                    double pixColor = (r == 255 && g == 255 && b == 255);

                    // Adds the pixel depending on how much it is on this pixel
                    if (((i + 1.f) * pixWidth) > posX && (j + 1.f) * pixHeight > posY)
                    { 
                        nbPix++;
                        color += pixColor;
                    }
                }
            }


            //matrix[i + j * CHAR_WIDTH] = color / (nbPix > 0 ? nbPix : 1);
            matrix[i + j * CHAR_WIDTH] = color > 0 ? 1 : 0;
        }
    }

    return matrix;
}

void saveTraining(double* charMatrix, size_t letterNumber)
{
    FILE* fp = fopen(training_file, "a");
    fprintf(fp, "%c --> ", EXPECTED_TEXT[letterNumber % strlen(EXPECTED_TEXT)]);
    for (size_t i = 0; i < CHAR_WIDTH * CHAR_HEIGHT; i++)
    {
        fprintf(fp, "%lg ", charMatrix[i]);
    }
    fprintf(fp, "\n");
    fclose(fp);
    printf("ADDED %c\n", EXPECTED_TEXT[letterNumber % strlen(EXPECTED_TEXT)]);

}

int* build_hist(SDL_Surface* img, size_t w, size_t h, size_t start)
{
    int* hist = malloc(sizeof(int) * w);
    for(size_t x = 0; x < w; x++)
    {
        int b_pix_count = 0;
        for (size_t y = start; y < h + start; y++)
        {
            Uint32 pixel = get_pixel(img, x, y);
            Uint8 r, g, b;
            SDL_GetRGB(pixel, img->format, &r, &g, &b);
            if (r == 0 && g == 0 && b == 0) b_pix_count++; // Black
        }
        hist[x] = b_pix_count;
    }
    return hist;
}

size_t space_threshold(SDL_Surface* img)
{
    size_t h = img->h;
    size_t w = img->w;
    size_t avg_line_gap = 0;
    size_t l_count = 0;
    size_t y = 0;

    
    while (y < h)
    {
        Uint32 pixel = get_pixel(img, 0, y);
        Uint8 r, g, b;
        SDL_GetRGB(pixel, img->format, &r, &g, &b);
        if (r==255 && g==255 && b==255) // Reads white
        {
            l_count++;
            size_t l_height = 0;
            char is_red = 0;
            while (!is_red)
            {
                pixel = get_pixel(img, 0, y + l_height);
                SDL_GetRGB(pixel, img->format, &r, &g, &b);
                if(r == 255 && g == 0 && b == 0)
                {
                    is_red = 1;
                    continue;
                }
                l_height++;
            }

            int* hist = build_hist(img, w, l_height, y);

            char in_gap = 0;
            int gapSize[w];
            size_t gap_count = 0;
            size_t curr_gap_count = 0;
            size_t t = 0;
            size_t t2 = 0;

            // Move to start of text
            while (hist[t] == 0) t++;

            for (; t < w; t++)
            {
                if (hist[t] == 0)
                {
                    curr_gap_count++;
                    if (in_gap == 0)
                    {
                        gap_count++;
                        in_gap = 1;
                    }
                } else {
                    if (in_gap == 1)
                    {
                        gapSize[t2++] = curr_gap_count;
                        in_gap = 0;
                        curr_gap_count = 0;
                    }
                }
            }

            if(in_gap) gap_count--;

            size_t total_gap = 0;
            for(size_t i = 0; i < gap_count; i++)
            {
                total_gap += gapSize[i];
            }

            size_t avg_gap = total_gap / (gap_count > 0 ? gap_count : 1);

            avg_line_gap = avg_line_gap + (size_t)avg_gap;

            y += l_height;

        } else {
            y++;
        }
    }

    return avg_line_gap / (l_count + 1);
}

//-----------------------------------------------------------------------------
// HORIZONTAL SEGMENTATION

void h_seg(SDL_Surface* img)
{
    size_t h = img->h;
    size_t w = img->w;

    for (size_t y = 0; y < h; y++)
    {
        for (size_t x = 0; x < w; x++)
        {
            Uint32 pixel = get_pixel(img, x, y);
            Uint8 r, g, b;
            SDL_GetRGB(pixel, img->format, &r, &g, &b);
            if(r == 0 && g == 0 && b == 0) break;
            if(x == w - 1) draw_hline(img, w, y);
        }
    }
}

//-----------------------------------------------------------------------------
// VERTICAL SEGMENTATION
char* char2string(char c)
{
    char *str=malloc(2*sizeof(char));
    if(str==NULL)
        return NULL;
    str[0]=c;
    str[1]='\0';
    return str;
}

void str_concatenation(char *str1,char* str2)
{
    char* tmp_str = str1;
    rendered_text = (char*)malloc(strlen(rendered_text)+1+strlen(tmp_str)+1);
    strcpy(rendered_text,tmp_str);
    strcat(rendered_text,str2);
}

char* v_seg(SDL_Surface* img, char isTrainingData)
{
    // Gets the size of the spaces
    size_t threshold = space_threshold(img);
    size_t letterNumber = 0;

    size_t arr_size = 0;
    
    size_t h = img->h;
    size_t w = img->w;

    size_t y = 0;
    
    // For each lines
    while (y < h)
    {
        Uint32 pixel = get_pixel(img, 0, y);
        Uint8 r, g, b;
        SDL_GetRGB(pixel, img->format, &r, &g, &b);
        if(r==255 && g==255 && b==255) // White
        {
            size_t l_height = 0;
            char is_red = 0;
            while (!is_red)
            {
                pixel = get_pixel(img, 0, y + l_height);
                SDL_GetRGB(pixel, img->format, &r, &g, &b);
                if (r == 255 && g == 0 && b == 0)
                {
                    is_red = 1;
                    break;
                }
                l_height++;
            }

            int* hist = build_hist(img, w, l_height, y);

            for (size_t i = 0; i < w; i++)
            {
                if (hist[i] == 0)
                {
                    draw_vline_red(img, i, y, y + l_height);
                    if (i > 1 && hist[i - 1] != 0)
                    {
                        arr_size++;
                    }
                }
            }

            int curr_char = 0;
            size_t x = 0;

            size_t w_count = 0;
            size_t eol = 0;
            size_t t = 1;

            while (hist[t] == 0) t++;
            draw_vline_yellow(img, t-1, y, y + l_height);
            for(; t < w; t++)
            {
                if (hist[t] == 0)
                {
                    w_count++;
                }
                else 
                {
                    //size_t eol = t;
                    if (w_count > threshold)
                    {
                        draw_vline_cyan(img, t - 1, y, y + l_height);
                        draw_vline_blue(img, t - w_count, y, y + l_height);
                    }
                    w_count = 0;
                }
            }

            draw_vline_green(img, eol + 1, y, y + l_height);
            str_concatenation(rendered_text,"\n");

            while(x < w)
            {
                pixel = get_pixel(img, x, y);
                SDL_GetRGB(pixel, img->format, &r, &g, &b);
                if (r == 0 && g == 255 && b == 255)
                {
                    str_concatenation(rendered_text," ");
                }
                size_t width = 0;
                if (hist[x] != 0)
                {
                    while (hist[x] != 0 && hist[x] < (int)w)
                    {
                        width++;
                        x++;
                    }
                    curr_char++;
                }

                if (l_height * width != 0) 
                {
                    //printf("%zu %zu %zu %zu\n", x, y, l_height, width);
                    double* charMatrix = build_char(img, x, y, width, l_height);
                    if (isTrainingData)
                    {
                        saveTraining(charMatrix, letterNumber);
                    } else {
                        char out = Guess(charMatrix);
                        //printf("%c", out);

                        str_concatenation(rendered_text,char2string(out));
                    }
                    free(charMatrix);
                    letterNumber++;
                }
                x++;
            }
            y += l_height;
            // Reads new line, if we are creating training data, this means reading a new expected text
            //letterNumber = 0;
            //printf("\n");
            free(hist);
        }
        else {
            y++;
        }
    }
    return rendered_text;
}

//-----------------------------------------------------------------------------
// DISPLAY

void display_segmentation(char* path, char debug, char isTrainingData)
{
    /* Displays the segmentation with additionnal infos if debug is true */
    init_sdl();

    imagePath = path;
    img = IMG_Load(path);

    if (debug) {
        create_renderer();
        render();
    }

    if(debug) {
        printf("STARTING BINARIZATION\n");
    }
    grayscale();
    if (debug)
        refresh_window(img);

    binarize();
    if (debug) 
    {
        refresh_window(img);
        wait_for_key_pressed();
        printf("BINARIZATION DONE\n");
    }


    if(debug) {
        wait_for_key_pressed();
        printf("STARTING HORIZONTAL SEGMENTATION\n");
    }
    h_seg(img);
    if (debug){
        refresh_window(img);
        printf("HORIZONTAL SEGMENTATION DONE\n");
        wait_for_key_pressed();
        printf("STARTING VERTICAL SEGMENTATION\n");
    }

    char* outputText = v_seg(img, isTrainingData);
    if (isTrainingData == 0)
    {
        printf("%s\n", outputText);
        rendered_text = "";
    }
    printf("VERTICAL SEGMENTATION DONE\n");
    if (debug)
    {
        refresh_window(img);
        wait_for_key_pressed();
        SDL_DestroyRenderer(renderer);
    }
}

