#include <stdio.h>
#include "power_of_two.h"

int main()
{
	for(unsigned long i=0; i<=63; i++)
	{
		unsigned long a = power_of_two((char)i);
		printf("power_of_two(%lu) = %lu\n",i,a);
	}
	return 0;
}
