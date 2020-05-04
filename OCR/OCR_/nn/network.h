#ifndef NEURALNETWORK_H
#define NEURALNETWORK_H

size_t* param;
size_t param_size;

double** biasList;
double*** weightList;

double learningRate;

char* filename;
char* training_file;

char* charList;

// Displays the current values
void Display();

// Frees all the variables used in the neural network
void FreeAll();

// Initialises the variables
void Init(size_t* p, size_t p_size, double lRate, char* name);

// Saves both weights and biases 
// (as well as network size and learning speed)
void Save(char* filename);

// Loads both weights and biases 
// (as well as network size and learning speed)
void Load(char* filename);

// Evaluates the input
char Guess(double* inputs);

// Trains the data repetition number of times, 
// and displays and save the stat
void Train(char* training_file, size_t repetition, char* exepected_text, size_t inputCount);

#endif