#include <stdio.h>

unsigned long isqrt(unsigned long n)
{
	unsigned long i = 1;
	unsigned long output = 1;

	if (n==0)
	{
		return 0;
	}
	else
	{
		while (output <= n)
		{
			i++;
			output = i*i;
		}
	}
	return i-1;
}
