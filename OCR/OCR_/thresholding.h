#ifndef THRESHOLDING_H
#define THRESHOLDING_H

#include "SDL2/SDL.h"
#include "SDL2/SDL_image.h"


/*
In the following variables you will find:

    1. As (unsigned int):
        - height and weight of the image
        - dimension of the image
        - the threshold
        - the probability of a pixel to 
          be in the first pixel class
        - the probability of a pixel to 
          be in the second pixel class
        - the sum of all the elements of 
          the histogram
        - an auxiliary variable that helps 
          to compute intra-class variances
    2. As (float): 
        - maximum variance
        - the mean of the first class
        - the mean of the second class
        - the between-class variance, 
          which is essential to compute 
          the optimal threshold
*/
unsigned int height, weight, threshold, 
             dimension_image, prob_first_class, 
             prob_second_class, sum, sumB; // initialized to 0

float var_max, mean_first_class, mean_second_class, 
      between_class_variance;

unsigned int *histogram;


/*********************************************************************/

/* Binarizes the image using the following pseudo/python code:
for pixel in image:
    if pixel > threshold:
        pixel = 1
    else:
        pixel = 0
*/
void binarize();


#endif