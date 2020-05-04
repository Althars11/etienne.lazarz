#include <stdio.h>
#include <err.h>
#include "SDL2/SDL.h"
#include "SDL2/SDL_image.h"       

SDL_Surface *img;
SDL_Window *screen;
SDL_Renderer *renderer;
SDL_Texture *texture;
char *path;

void init_sdl()
{
    if(SDL_Init(SDL_INIT_VIDEO) == -1)
        errx(1,"Could not initialize SDL: %s.\n", SDL_GetError());
}

void load_image()
{
    img = IMG_Load(path);
}

void create_window_texture()
{
    screen = SDL_CreateWindow("Displaying",
                        SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, img->w, img->h, 0);
    renderer = SDL_CreateRenderer(screen, -1, 0);

    texture = SDL_CreateTextureFromSurface(renderer, img);
}

void render()
{
    SDL_RenderCopy(renderer, texture, NULL, NULL);
    SDL_RenderPresent(renderer);
}

void wait_for_key_pressed()
{
    SDL_Event event;
    
    // Wait for a key to be down
    do
    {
        SDL_PollEvent(&event);
    } while(event.type != SDL_KEYDOWN);

    // Wait for a key to be up.
    do
    {
        SDL_PollEvent(&event);
    } while(event.type != SDL_KEYUP);
}


void free_image_window()
{
    SDL_DestroyTexture(texture);  
    SDL_FreeSurface(img);  
    SDL_DestroyRenderer(renderer);  
    SDL_DestroyWindow(screen);

    SDL_Quit();   
}

void display()        
{       
    init_sdl();
    path = "Try.jpg";
    img = IMG_Load(path);       
    create_window_texture();
    render();

    wait_for_key_pressed();
    free_image_window();
}

int main()
{
    display();
    return 0;
}