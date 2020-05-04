#ifndef SEGMENTATION_H
#define SEGMENTATION_H

char* training_file;
char* EXPECTED_TEXT;

size_t CHAR_WIDTH;
size_t CHAR_HEIGHT;

void display_segmentation(char* path, char debug, char isTrainingData);

#endif