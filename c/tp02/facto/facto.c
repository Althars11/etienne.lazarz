#include <stdio.h>

unsigned long facto(unsigned long n)
{
	if (n==0)
	{
		return 1;
	}
	else
	{
		unsigned long fact = 1;
		for(unsigned long i=1; i <= n; i++)
		{
			fact = fact*i;
		}

	return fact;
	}
}
