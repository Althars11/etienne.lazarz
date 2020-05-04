#include <stdio.h>
#include <stdlib.h>
#include <err.h>
#include "divisor_sum.h"

int main(int argc, char** argv)
{
	unsigned long param = strtoul(argv[1], NULL, 10);
	unsigned long p =divisor_sum(param);
	switch(param)
	{
		case (argc):
			if(argc==int)
			printf("divisor_sum(%lu) = %lu",param,p);
		break;

		default:
			errx("main: Error");
		break;
	}
}
