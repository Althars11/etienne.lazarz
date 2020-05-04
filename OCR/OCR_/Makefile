#Simple SDL mini Makefile

CC=gcc

CPPFLAGS= `pkg-config --cflags sdl2` -MMD
CFLAGS= -Wall -Wextra -Werror -std=c99 -O3
LDFLAGS=
LDLIBS= `pkg-config --libs sdl2` -lSDL2_image -lm

all: main

main: segmentation.o thresholding.o grayscale.o nn/network.o
segmentation.o: segmentation.h grayscale.o nn/network.o
grayscale.o: grayscale.h
nn/network.o : nn/network.h
thresholding.o : thresholding.h

clean:
	${RM} *.o
	${RM} *.d
	${RM} segmentation

# END
