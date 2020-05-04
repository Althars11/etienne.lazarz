#include <stdio.h>
#include "isqrt.h"

int main()
{
	for(unsigned long i=0; i<=200; i+=8)
	{
	 	unsigned long a = isqrt(i);
		printf("isqrt(%lu) = %lu\n",i,a);
	}
	return 0;
}
