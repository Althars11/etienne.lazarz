#include <stdio.h>
#include "digit_count.h"

int main()
{
	for(unsigned long i=0; i<=63; i++ )
	{
		if(i==0)
		{
			printf("digit_count(%lu) = 1 \n",i);
		}
		else
		{
			unsigned long from = 1<<i;
		 	unsigned long answer = digit_count(from);
			printf("digit_count(%lu) = %lu \n",from,answer);
		}
	}
	return 0;
}
