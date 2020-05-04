#include <stdio.h>
#include <stdlib.h>
#include <err.h>
#include <math.h>
#include "thresholding.h"
#include "grayscale.h"

// Auxiliary function that initializes the histogram
void initialize_histogram()
{
    histogram = calloc(256, sizeof(int));
    for (size_t i = 0; i < 256; i++)
        histogram[i] = 0;
}

void initialize(SDL_Surface *image)
{
    height = image->h;
    weight = image->w;
    threshold = 0;
    dimension_image = height*weight;
    initialize_histogram();
    var_max = 0.0;
    prob_first_class = 0;
    prob_second_class = 0;
    sum = 0;
    sumB = 0;
    mean_first_class = 0.0;
    mean_second_class = 0.0;
}

void compute_histogram(SDL_Surface *image)
{
    for (unsigned int i = 0; i < height; i++)
    {
        for (unsigned int j = 0; j < weight; j++)
        {
            Uint32 pixel = get_pixel(image, j, i);
            Uint8 r, g, b;
            SDL_GetRGB(pixel, img->format, &r, &g, &b);
            histogram[(unsigned char)pixel]++;
        } 
    }
}

void compute_sum()
{
    for (unsigned int i = 0; i < 256; i++)
        sum += i*histogram[i];
}

void update_prob_first_class(unsigned int t)
{
    prob_first_class += histogram[t];
}

void update_prob_second_class()
{
    prob_second_class = dimension_image - prob_first_class;
}

void update_class_means(unsigned int t)
{
    sumB += t*histogram[t];
    mean_first_class = ((float)sumB)/((float)prob_first_class);
    mean_second_class = ((float)(sum-sumB))/((float)prob_second_class);
}

void update_between_class_variance()
{
    between_class_variance = 
    prob_first_class*prob_second_class*
    pow(mean_first_class-mean_second_class, 2);
}

void set_threshold(unsigned int t)
{
    if (between_class_variance > var_max)
    {
		if (t < 20)
            t += 100;
        else
        {
            if (t < 80)
                t += 60;
        }
        
        threshold = t;
        var_max = between_class_variance;
    }
}

void thresholding(SDL_Surface *image)
{
    initialize(image);
    compute_histogram(image);
    compute_sum();
    for (unsigned int t = 0; t < 256; t++)
    {
        update_prob_first_class(t);
        if (!prob_first_class)
            continue;
        update_prob_second_class();
        
        update_class_means(t);
        update_between_class_variance();

        set_threshold(t);
    }
}

void binarize()
{
    thresholding(img);
    
    for (unsigned int i = 0; i < height; i++)
    {
        for (unsigned int j = 0; j < weight; j++)
        {
            Uint32 pixel = get_pixel(img, j, i);
            Uint8 r, g, b;
            SDL_GetRGB(pixel, img->format, &r, &g, &b);

            Uint8 pixel_update = ((unsigned char)pixel) <= threshold?0:255;
            pixel = SDL_MapRGB(img->format, pixel_update, pixel_update, pixel_update);
            put_pixel(img, j, i, pixel);
        }
    }
}
