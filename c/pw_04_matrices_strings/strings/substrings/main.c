#include <stdio.h>
#include <err.h>

size_t str_len(char s[])
{
    size_t i = 0;
    while (s[i]!=0)
    {
        i++;
    }
    return i;
}

int is_a_good_input(char s[])
{
    char index[] = "^éèàïäüù";
    size_t lens = str_len(s);   
    size_t lenindex = str_len(index);
    int is_ok = 0;
    int i = 0;
    while (is_ok && i < lens)
    {
        for (size_t x=0; x < lenindex;x++)
        {
            if (index[x]==s[i])
            {
                is_ok = 1;
            }   
        }
        i++;   
    }
    return is_ok;
}



int main(char s[], char substring[])
{
    size_t lens= str_len(s);
    size_t lensub = str_len(substring);

    if (is_a_good_input(s)==1)
    {
        errx(1,"accent");
    }
    else
    {
        for (size_t x = 0; x < lens-lensub;x++)
        {
            if (s[x]==substring[0])
            {
                int i = 1;
                int is_ok = 0;
                while (is_ok && i < lensub)
                {
                    if (substring[i]!=s[x+i])
                    {
                        is_ok = 1;
                        printf("Not Found!");
                    }
                    else
                    {
                        i++;
                    }   
                }
                if (is_ok)
                {
                    printf("%s\n",s);
                    char retour[x];
                    for(size_t y = 0; y < x-1 ; x++)
                    {
                        retour[y] = " ";
                    }
                    retour[x] = "^";
                    printf("%s\n",retour);
                }
            }
        }
        return 0;
    }
}