#include <stdio.h>
#include "facto.h"

int main()
{
	for(unsigned long i=0; i<=20; i++)
	{
	 	unsigned long a = facto(i);
		printf("facto(%lu) = %lu\n",i,a);
	}
	return 0;
}
