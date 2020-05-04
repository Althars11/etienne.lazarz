#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <time.h>
#include <math.h>

#include "network.h"

size_t* param;
size_t param_size;

double** biasList;
double*** weightList;

double learningRate;

char* filename;

///////////////////////////////////////////////////////////////////////////////

double activation(double val)
{
	// addaped ReLI
	// return val > 0 ? val : ALPHA * val // ALPHA needs to be defined

	// Sigmoid
	return 1 / (1 + exp(-val));
}

double getRandom(double min, double max)
{
	if (min == max) return min;
	double scale = rand() / (double) RAND_MAX;
	return min + scale * (max - min);
}

///////////////////////////////////////////////////////////////////////////////

// Build array and matrix
double* BuildArray(size_t size)
{
	return malloc(sizeof(double) * size);
}

double** BuildMatrix(size_t x, size_t y)
{
	double** matrix = malloc(sizeof(double) * x);
	for (size_t i = 0; i < x; i++)
	{
		matrix[i] = BuildArray(y);
	}
	return matrix;
}

///////////////////////////////////////////////////////////////////////////////

void DrawArray(double* array, size_t size)
{
	printf("[");
	for (size_t i = 0; i < size; i++)
	{
		printf("%.17g ", array[i]);
	}
	printf("]\n");
}

void DrawMatrix(double** matrix, size_t x, size_t y)
{
	printf("[\n");
	for (size_t j = 0; j < x; j++)
	{
		printf("\t[");
		for (size_t i = 0; i < y; i++)
		{
			printf("%.17g ", matrix[i][j]);
		}
		printf("]\n");
	}
	printf("]\n");
}

// Display State
void Display()
{
	printf("DISPLAY\n");
	printf("[");for (size_t i = 0; i < param_size; i++){printf("%zu ", param[i]);}printf("] ");

	printf("Learning rate: %.17g\n", learningRate);

	printf("WEIGHTS : \n");
	for (size_t i = 0; i < param_size - 1; i++)
	{
		printf("LAYER %zu:", i);
		DrawMatrix(weightList[i], param[i + 1], param[i]);
	}

	printf("BIAS : \n");
	for (size_t i = 0; i < param_size - 1; i++)
	{
		printf("LAYER %zu: ", i);
		DrawArray(biasList[i], param[i + 1]);
	}
}

///////////////////////////////////////////////////////////////////////////////

void FreeMatrix(double** matrix, size_t y)
{
	for (size_t i = 0; i < y; i++)
	{
		free(matrix[i]);
	}
	free(matrix);
}

void FreeWeights(double*** weights)
{
	for (size_t i = 0; i < param_size - 1; i++)
	{
		FreeMatrix(weights[i], param[i]);
	}
	free(weights);
}

void FreeAll()
{
	FreeMatrix(biasList, param_size - 1);
	FreeWeights(weightList);
}

///////////////////////////////////////////////////////////////////////////////

void InitBias()
{
	biasList = malloc(sizeof(double*) * param_size - 1);
	for (size_t i = 0; i < param_size - 1; i++)
	{
		double* b = BuildArray(param[i + 1]);
		for(size_t j = 0; j < param[i + 1]; j++)
		{
			b[j] = getRandom(-1, 1);
		}
		biasList[i] = &b[0];
	}
}

void InitWeights()
{
	weightList = malloc(sizeof(double**) * param_size - 1);
	for (size_t i = 0; i < param_size - 1; i++)
	{
		double** w = BuildMatrix(param[i], param[i + 1]);
		for (size_t j = 0; j < param[i]; j++)
		{
			for (size_t k = 0; k < param[i + 1]; k++)
			{
				w[j][k] = getRandom(-1, 1);
			}
		}
		weightList[i] = &w[0];
	}
}

// Initialises the variables
void Init(size_t* p, size_t p_size, double lRate, char* name)
{
	param = p;
	param_size = p_size;	

	learningRate = lRate;

	filename = name;

	// Seed rand()
	srand(time(NULL));

	InitBias();
	InitWeights();
}

///////////////////////////////////////////////////////////////////////////////

void Save(char* filename)
{
	FILE* fp = fopen(filename, "w");
	fprintf(fp, "#%zu, ", param_size);
	for (size_t i = 0; i < param_size; i++)
		fprintf(fp, "%zu ", param[i]);
	fprintf(fp, "\n#%.17g\n", learningRate);

	for (size_t i = 0; i < param_size - 1; i++)
	{
		for(size_t j = 0; j < param[i]; j++)
		{
			for(size_t k = 0; k < param[i + 1]; k++)
				fprintf(fp, "%.17g ", weightList[i][j][k]);
			fprintf(fp, "\n");
		}
		fprintf(fp, "\n");
		for(size_t j = 0; j < param[i + 1]; j++)
			fprintf(fp, "%.17g ", biasList[i][j]);
		fprintf(fp, "\n\n");
	}

	fclose(fp);
}

void Load(char* filename)
{
	int overload;
	FILE* fp = fopen(filename, "r");

	size_t param_size;
	overload = fscanf(fp, "#%zu, ", &param_size);

	size_t* param = malloc(sizeof(size_t) * param_size);
	for (size_t i = 0; i < param_size; i++)
		overload = fscanf(fp, "%zu ", &param[i]);

	double learningRate;
	overload = fscanf(fp, "\n#%lg", &learningRate);

	Init(param, param_size, learningRate, filename);

	for (size_t i = 0; i < param_size - 1; i++)
	{
		for(size_t j = 0; j < param[i]; j++)
		{
			for(size_t k = 0; k < param[i + 1]; k++)
				overload = fscanf(fp, "%lg ", &weightList[i][j][k]);
			overload = fscanf(fp, "\n");
		}
		overload = fscanf(fp, "\n");
		for(size_t j 	= 0; j < param[i + 1]; j++)
			overload = fscanf(fp, "%lg ", &biasList[i][j]);
		overload = fscanf(fp, "\n");
	}

	//ignores overload
	overload ? fclose(fp) : fclose(fp);
}

///////////////////////////////////////////////////////////////////////////////

double* Compute(double* inputs)
{
	double** val = malloc(sizeof(double*) * param_size);
	for (size_t i = 0; i < param_size; i++)
	{
		double* layer = malloc(sizeof(double) * param[i]);
		val[i] = &layer[0];
	}
	for (size_t i = 0; i < param[0]; i++)
		val[0][i] = inputs[i];

	for (size_t i = 1; i < param_size; i++)
	{
		for (size_t j = 0; j < param[i]; j++)
		{
			val[i][j] = biasList[i - 1][j];
			for (size_t k = 0; k < param[i - 1]; k++)
			{
				val[i][j] += weightList[i - 1][k][j] * val[i - 1][k];
			}
			val[i][j] = activation(val[i][j]);
		}
	}

	double* out = &val[param_size - 1][0];
	for (size_t i = 0; i < param[param_size - 1]; i++)
	{
		out[i] = val[param_size - 1][i];
	}
	FreeMatrix(val, param_size - 1);
	return out;
}

char Guess(double* inputs)
{
	double* out = Compute(inputs);

	double max = out[0];
	size_t index = 0;
	for (size_t i = 1; i < param[param_size - 1]; i++)
	{
		if(out[i] > max)
		{
			index = i;
			max = out[i];
		}
	}
	free(out);
	index = index == 0 ? index-1 : index;
	return (char) (index + 34);
}

///////////////////////////////////////////////////////////////////////////////

double TrainSingle(double* inputs, double* expected)
{
	// Compute the values of the two layers
	double** val = malloc(sizeof(double*) * param_size);
	for (size_t i = 0; i < param_size; i++)
	{
		double* layer = malloc(sizeof(double) * param[i]);
		val[i] = &layer[0];
	}
	for (size_t i = 0; i < param[0]; i++)
		val[0][i] = inputs[i];

	for (size_t i = 1; i < param_size; i++)
	{
		for (size_t j = 0; j < param[i]; j++)
		{
			val[i][j] = biasList[i - 1][j];
			for (size_t k = 0; k < param[i - 1]; k++)
			{
				val[i][j] += weightList[i - 1][k][j] * val[i - 1][k];
			}
			val[i][j] = activation(val[i][j]);
		}
	}
	
	// ------------------------------------------------------------------------
	// Output
	double** error = malloc(sizeof(double*) * param_size - 1);
	error[param_size - 2] = BuildArray(param[param_size - 1]);

	double cost = 0;
	for (size_t i = 0; i < param[param_size - 1]; i++)
	{
		error[param_size - 2][i] = (expected[i] - val[param_size - 1][i]);
		cost += pow(val[param_size - 1][i] - expected[i], 2);
		val[param_size - 1][i] = (val[param_size - 1][i] * 
			(1 - val[param_size - 1][i])) * error[param_size - 2][i] * 
			learningRate;
	}

	double*** delta = malloc(sizeof(double**) * param_size - 1);
	delta[param_size - 2] = BuildMatrix(param[param_size - 2], 
		param[param_size - 1]);
	for (size_t i = 0; i < param[param_size - 2]; i++)
	{	
		for (size_t j = 0; j < param[param_size - 1]; j++)
		{
			delta[param_size - 2][i][j] = val[param_size - 2][i] * 
				val[param_size - 1][j];
		}
	}

	// Update weights
	for (size_t i = 0; i < param[param_size - 2]; i++)
	{	
		for (size_t j = 0; j < param[param_size - 1]; j++)
		{
			weightList[param_size - 2][i][j] += delta[param_size - 2][i][j];
		}
	}
	for (size_t i = 0; i < param[param_size - 1]; i++)
	{
		biasList[param_size - 2][i] += val[param_size - 1][i];
	}

	// Hidden
	for (size_t layer = param_size - 2; layer > 0; layer--)
	{
		error[layer - 1] = BuildArray(param[layer]);
		for(size_t i = 0; i < param[layer]; i++)
		{
			error[layer - 1][i] = 0;
			for (size_t j = 0; j < param[layer + 1]; j++)
			{
				error[layer - 1][i] += error[layer][j] * 
					weightList[layer][i][j];
			}
		}

		// Compute the gradient
		for (size_t i = 0; i < param[layer]; i++)
		{
		val[layer][i] = (val[layer][i] * (1 - val[layer][i])) *
			error[layer - 1][i] * learningRate;
		}

		delta[layer - 1] = BuildMatrix(param[layer - 1], param[layer]);
		for (size_t i = 0; i < param[layer - 1]; i++)
		{
			for (size_t j = 0; j < param[layer]; j++)
			{
				delta[layer - 1][i][j] = val[layer - 1][i] * val[layer][j];
			}
		}

		// Update weight and bias
		for (size_t i = 0; i < param[layer - 1]; i++)
		{
			for (size_t j = 0; j < param[layer]; j++)
			{
				weightList[layer - 1][i][j] += delta[layer - 1][i][j];
			}
		}
		for (size_t i = 0; i < param[layer]; i++)
		{
			biasList[layer - 1][i] += val[layer][i];
		}
	}


	for (size_t i = 0; i < param_size - 1; i++)
	{
		FreeMatrix(delta[i], param[i]);
	}
	free(delta);
	FreeMatrix(error, param_size - 1);
	FreeMatrix(val, param_size);
	return cost;
}

///////////////////////////////////////////////////////////////////////////////

void TrainEpoch(double** inputs, double** expected, size_t inputCount, char display)
{
	double totalCost = 0;
	for (size_t i = 0; i < inputCount; i++)
	{
		totalCost += TrainSingle(inputs[i], expected[i]);
	}
	double cost = totalCost / inputCount;
	if (display) printf("Mean cost : %.17g\n", cost);
}

void Train(char* training_file, size_t repetition, char* expected_text, size_t inputCount)
{
	double** inputs = malloc(sizeof(double*) * inputCount);
	double** expected = malloc(sizeof(double*) * inputCount);
	int overload = 0;

	FILE* fp = fopen(training_file, "r");
	for(size_t i = 0; i < inputCount; i++)
	{
		char out;
		overload = fscanf(fp, "%c --> ", &out);

		double* outs = malloc(sizeof(double) * param[param_size - 1]);
		for (size_t j = 0; j < param[param_size - 1]; j++)
 		{

 			outs[j] = (double)(out == expected_text[j]);
 		}
 		expected[i] = &outs[0];
 		double* ins = malloc(sizeof(double) * param[0]);
 		for (size_t j = 0; j < param[0]; j++)
 		{
 			overload = fscanf(fp, "%lg ", &ins[j]);
 		}
 		inputs[i] = &ins[0];
 		overload = fscanf(fp, "\n");
	}
	
	overload ? fclose(fp) : fclose(fp);

	for(size_t i = 0; i < repetition; i++)
	{
		TrainEpoch(inputs, expected, inputCount, i % 10 == 0);
		if (i % 10 == 0) {printf("Saving to %s \n", filename); Save(filename);}
	}
	FreeMatrix(inputs, param[0]);
	FreeMatrix(expected, param[param_size - 1]);
}

///////////////////////////////////////////////////////////////////////////////

// int main(int argc, char** argv)
// {
// 	int repetition = 1;
// 	char load = 1;

// 	size_t param[] = {16, 16, 16, 2};
// 	size_t param_size = 4;
// 	learningRate = 0.1f;

// 	filename = "weights.txt";

// 	if (argc > 1) {
// 		repetition = atoi(argv[1]);
		
// 		if (argc > 2) {
// 			filename = argv[2];
// 			if (argc > 3) {
// 				load = atoi(argv[3]);
// 			}
// 		}
// 	}

// 	if (load == 0) {
// 		printf("LOADING FROM FILE %s\n", filename);
// 		Load(filename);
// 	} else {
// 		Init(param, param_size, learningRate);
// 	}

// 	double** inputs = malloc(sizeof(double*) * (1 << param[0]));
// 	double** expected = malloc(sizeof(double*) * (1 << param[0]));

// 	for (size_t i = 0; i < (size_t)(1 << param[0]); i++)
// 	{
// 		double* in = malloc(sizeof(double) * param[0]);
// 		double* e = malloc(sizeof(double) * param[param_size - 1]);
// 		char a = 0;
// 		for (size_t j = 0; j < param[0]; j++)
// 		{
// 			in[j] = ((i >> j) & 1);
// 			a ^= (char) in[j]; 
// 		}
// 		inputs[i] = &in[0];
// 		for (size_t j = 0; j < param[param_size - 1]; j++)
// 		{
// 			e[j] = (a == (char)j ? 0.f : 1.f);
// 		}
// 		expected[i] = &e[0];
// 	}

// 	Train(inputs, expected, (1 << param[0]), repetition);	

// 	for (size_t i = 0; i < (size_t)(1 << param[0]); i++)
// 	{
// 		printf("Testing %zu, expected: \n", i);
// 		DrawArray(inputs[i], param[0]);
// 		DrawArray(expected[i], param[param_size - 1]);
// 		printf("Got :\n");
// 		double* layer = Compute(inputs[i]);
// 		DrawArray(layer, param[param_size - 1]);
// 		free(layer);
// 	}

// 	Save(filename);


// 	FreeMatrix(inputs, (1 << param[0]));
// 	FreeMatrix(expected, (1 << param[0]));

// 	//Display();

// 	FreeAll();
// 	return 0;
// }