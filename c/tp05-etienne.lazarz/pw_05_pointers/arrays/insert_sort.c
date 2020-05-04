#include <helper.h>
#include <stdlib.h>
#include <stdio.h>


// Insertion using the plain algorithm.
void array_insert(int *begin, int *end, int x)
{
    int* index = end;

    while (index > begin && x < *(index-1))
    {
        *index = *(index-1);
        index--;
    }
    *index = x;
}


// Insertion using the binary-search algorithm.
void array_insert_bin(int *begin, int *end, int x)
{
    int* index = end;
    int i = is_present(*begin,*end, x);

    while (index > i)
    {
        *index = *(index-1);
        index--;
    }
    *index = x;
}

// Insertion sort using plain method.
void array_insert_sort(int *begin, int *end)
{
    for (int *p = begin; p < end; p++)
    {
        array_insert(*begin,*end,*p);
    }
}


// Insertion sort using binary search.
void array_insert_sort_bin(int *begin, int *end)
{
    
}

