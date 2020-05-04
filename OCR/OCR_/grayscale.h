#ifndef GRAYSCALE_H
#define GRAYSCALE_H

#include <SDL2/SDL.h>
#include <SDL2/SDL_image.h>

SDL_Surface *img;
SDL_Window *screen;
SDL_Renderer *renderer;
SDL_Texture *texture;
char *path;

/************************************ INTERMEDIARY FUNCTIONS ******************************/

Uint8* pixel_ref(SDL_Surface *surf, unsigned x, unsigned y);

Uint32 get_pixel(SDL_Surface *surface, unsigned x, unsigned y);

void put_pixel(SDL_Surface *surface, unsigned x, unsigned y, Uint32 pixel);

void free_image_window();

/******************************************************************************************/

void init_sdl();

void create_renderer();

void render();

void wait_for_key_pressed();

void grayscale();

void display();

#endif