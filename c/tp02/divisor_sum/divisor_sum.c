#include <stdio.h>

unsigned long divisor_sum(unsigned long n)
{
	unsigned long i = 0; 
	unsigned long resultat = 0;
	for (i=1; i<=n/2; i++)
	{
		if (n % i == 0)
		{
			resultat = resultat + i;
		}
	}
	return resultat;
}
