using System;
using System.Collections.Generic;

namespace Brainfuck
{
    class Brainfuck
    {
        
        public static string Interpret(string code, Dictionary<char, char> symbols)
        {
            int pointeur_p = 0;
            string sortibf = "";
            byte[] BFarray = new byte[30000];
            int i = 0;
            int len = code.Length;
            
            
            while (i<len)
            {
                if (code[i] == symbols['>'])
                {
                    if (pointeur_p == 29999)
                    {
                        pointeur_p = 0;
                    }
                    pointeur_p++;
                }
                else if (code[i]== symbols['<'])
                {
                    if (pointeur_p == 0)
                    {
                        pointeur_p = 29999;
                    }
                    pointeur_p--;
                }
                else if (code[i] == symbols['+'])
                {
                    if (BFarray[pointeur_p] == 255)
                    {
                        BFarray[pointeur_p] = 0;
                    }
                    BFarray[pointeur_p]++;
                }
                else if (code[i]== symbols['-'])
                {
                    if (BFarray[pointeur_p] == 0)
                    {
                        BFarray[pointeur_p] = 255;
                    }
                    BFarray[pointeur_p]--;
                }
                else if (code[i]== symbols['.'])
                {
                    sortibf += (char)(BFarray[pointeur_p]);
                }
                else if (code[i]== symbols[','])
                {
                    BFarray[pointeur_p] = (byte) Console.ReadKey().KeyChar;
                }
                else if (code[i]== symbols['['])
                {
                    if (BFarray[pointeur_p] == 0)
                    {
                        int loop = 1;
                        while (i<len && loop != 0)
                        {
                            i ++;
                            if (code[i] == '[')
                            {
                                loop ++;
                            }
                            else
                            if (code[i] == ']')
                            {
                                loop --;
                            }
                        }
                    }
                }
                else if (code[i]== symbols[']'])
                {
                    if (BFarray[pointeur_p]!=0)
                    {
                        int loop = 1;
                        while (i >= 0 && loop != 0)
                        {
                            i--;
                            if (code[i] == '[')
                            {
                                loop--;
                            }
                            else if (code[i] == ']')
                            {
                                loop++;
                            }
                        }
                    }
                }

                i++;
            }
            return sortibf;
        }
        public static string GenerateCodeFromText(string text, Dictionary<char, char> symbols)
        {
            int pointeur_p = 0;
            string sortibf = "";
            int[] BFarray = new int[30000];
            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                BFarray[i] = text[i];
            }
            for (int i = 0; i < len; i++)
            {
                sortibf += ">";
                while (BFarray[i]>0)
                {
                    sortibf += "+";
                    BFarray[i]--;
                }

                sortibf += ".";
            }
            return sortibf;
        }

        public static string ShortenCode(string program, Dictionary<char, char> symbols)
        {
            int count = 0;
            for (int i = 0; i < program.Length; i++)
            {
                if (program[i]=='+')
                {
                    count++;
                }

                if (program[i]!='+' && count>)
                {
                    
                }
            }
            return "Not implemented";
        }
    }
}
