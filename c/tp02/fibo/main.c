#include <stdio.h>
#include "fibo.h"

int main()
{
	for(size_t i=0; i<=90; i++)
	{
	 	unsigned long a = fibo(i);
		printf("fibo(%lu) = %lu\n",i,a);
	}
	return 0;
}
