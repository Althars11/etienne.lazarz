#include <stdio.h>

unsigned char digit_count(unsigned long n)
{
	unsigned char i = 1;
	unsigned long div = 10;
	while(n/div >= 1)
	{
		i++;
		div = div*10;
	}
	return (unsigned char)i;
}

