#include <stdio.h>

void print_matrix(char s[], double m[], size_t rows, size_t cols)
{
    printf("%s =\n",s);

    for (size_t y = 0; y < rows; y++)
    {
        for (size_t x = 0; x < cols; x++)
        {
            if (x==cols-1)
            {
                printf(" %4g \n", m[y * cols + x]);
            }
            else
            {
                printf(" %4g",m[y * cols + x]);
            }
        }
    }   
}

void transpose(double m[], size_t rows, size_t cols, double r[])
{
    for (size_t y = 0; y < rows; y++)
    {
        for (size_t x = 0; x < cols; x++)
        {
            r[x * rows + y] = m[y * cols + x];
        }
    }
    char name[] = "m_tr";
    print_matrix(name,r,cols,rows);
}

void add(double m1[], double m2[], size_t rows, size_t cols, double r[])
{
    for (size_t y = 0; y < rows; y++)
    {
        for (size_t x = 0; x < cols; x++)
        {
            r[y * cols + x] = m1[y * cols + x] + m2[y * cols + x];
        }
    }

    print_matrix("add m",r,rows,cols);
}

void mul(double m1[], double m2[], size_t r1, size_t c1, size_t c2, double r[])
{
    for (size_t y = 0; y < r1; y++)
    {
        for (size_t x = 0; x < c2; x++)
        {
            for (size_t k = 0; k < c1; k++)
            {
                r[y * c1 + x] += m1[y * c1 + k]*m2[k * c2 + x];
            }
        }
    }
    char name[] = "m1_times_m2 =\n";
    print_matrix(name,r,r1,c2);
}
