#include <err.h>
#include "vector.h"

struct vector *vector_new()
{
    size_t capacity = 1;
    size_t size = 0;
    
}

void vector_free(struct vector *v)
{
    // TODO
}

void double_capacity(struct vector *v)
{
    // TODO
}

void vector_push(struct vector *v, int x)
{
    // TODO
}

int vector_pop(struct vector *v, int *x)
{
    // TODO
}

int vector_get(struct vector *v, size_t i, int *x)
{
    // TODO
}

void vector_insert(struct vector *v, size_t i, int x)
{
    // TODO
}

int vector_remove(struct vector *v, size_t i, int *x)
{
    // TODO
}

int main()
{
    return 0;
}
