using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Tiny42sh
{
    public enum Keyword
    {
        ls,
        cd,
        cat,
        touch,
        rm,
        mkdir,
        pwd,
        clear,
        exit,
        NOT_A_KEYWORD
    }

    public static class Interpreter
    {
        static public string readline()
        {
            return Console.ReadLine();
        }

        static public string[] parse_input(string input)
        {
            string[] mycommand = input.Split();
            return mycommand;
        }

        static public Keyword is_keyword(string word)
        {
            switch (word)
            {
                case "ls":
                    return Keyword.ls;
                case  "cd":
                    return Keyword.cd;
                case "cat":
                    return Keyword.cat;
                case "touch":
                    return Keyword.touch;
                case "rm":
                    return Keyword.rm;
                case "mkdir":
                    return Keyword.mkdir;
                case "pwd":
                    return Keyword.pwd;
                case "clear":
                    return Keyword.clear;
                case "exit":
                    return Keyword.exit;
                default:
                    return Keyword.NOT_A_KEYWORD;
            }
        }
    }
}