using System;
using System.IO;
//using static Tiny42sh.Keyword;
//using static System.IO.Directory;
using System.Collections.Generic;

namespace Tiny42sh
{
    public static class Execution
    {
        static public int execute_commad(string[] cmd)
        {
            string currentkeyword = cmd[0];
            
            switch (Interpreter.is_keyword (currentkeyword))
            {
                 case Keyword.ls:
                     return execute_ls(cmd);
                 case Keyword.cd:
                     return execute_cd(cmd);
                 case Keyword.cat:
                     return execute_cat(cmd);
                 case Keyword.touch:
                     return execute_touch(cmd);
                 case Keyword.rm:
                     return execute_rm(cmd);
                 case Keyword.mkdir:
                     return execute_mkdir(cmd);
                 case Keyword.pwd:
                     return execute_pwd(cmd);
                 case Keyword.clear:
                     return execute_clear(cmd);
                 case Keyword.exit:
                     return execute_exit(cmd);
                 default:
                     Console.Error.WriteLine("not a command");
                     return 1;
            }
        }

        static private int execute_ls(string[] cmd)
        {
            string path = "";
            if (cmd.Length>1)
            {
                for (int i = 1; i < cmd.Length; i++)
                {
                    path = cmd[i];
                    if (Directory.Exists(path))
                    {
                        foreach (var e in Directory.EnumerateFileSystemEntries(path))
                        {
                            if (Directory.Exists(e))
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine(Path.GetFileName(e));
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine(Path.GetFileName(e));
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("indicated path does not match any");
                        return 1;
                    }
                }
            }
            else
            {
                foreach (var e in Directory.EnumerateFileSystemEntries(Directory.GetCurrentDirectory()))
                {
                    Console.WriteLine(Path.GetFileName(e));
                }
            }
            
            return 0;
        }

        static private int execute_cd(string[] cmd)
        {
            if (cmd.Length != 2)
            {
                Console.Error.WriteLine("cd should be called with 1 argument but is called with {0}", cmd.Length - 1);
                return 1;
            }
            else
            {
                if (Directory.Exists(cmd[1]))
                {
                    Directory.SetCurrentDirectory(cmd[1]);
                }
                else
                {
                    Console.Error.WriteLine("path {0} does not match any",cmd[1]);
                    return 1;
                }
            }
            return 0;
        }

        static private int execute_cat(string[] cmd)
        {
            if (cmd.Length > 1)
            {
                for (int i = 1; i < cmd.Length; i++)
                {
                    string path = cmd[i];
                    if (File.Exists(path))
                    {
                        foreach (var line in File.ReadAllLines(path))
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("file {0} does not exist or is a directory", cmd[i]);
                        return 1;
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("rm should have arguments");
                return 1;
            }
            return 0;
        }

        static private int execute_touch(string[] cmd)
        {
            if (cmd.Length > 1)
            {
                for (int i = 1; i < cmd.Length; i++)
                {
                    if (File.Exists(cmd[i]))
                    {
                        File.SetLastAccessTime(cmd[i], DateTime.Now);
                    }
                    else
                    {
                        File.Create(cmd[i]);
                    }
                } 
            }
            else
            {
                Console.Error.WriteLine("rm should have arguments");
                return 1;
            }
            return 0;
        }

        static private int execute_rm(string[] cmd)
        {
            if (cmd.Length>1)
            {
                for (int i = 1; i < cmd.Length; i++)
                {
                    if (File.Exists(cmd[i]))
                    {
                        File.Delete(cmd[i]);
                    }
                    else
                    {
                        Console.Error.WriteLine("file {0} does not exist",cmd[i]);
                        return 1;
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("rm should have arguments");
                return 1;
            }
            return 0;
        }

        static private int execute_mkdir(string[] cmd)
        {
            if (cmd.Length>1)
            {
                for (int i = 1; i < cmd.Length; i++)
                {
                    if (Directory.Exists(cmd[i]))
                    {
                        Console.Error.WriteLine("Directory {0} already exist",cmd[i]);
                        return 1;
                    }
                    else
                    {
                        Directory.CreateDirectory(cmd[i]);
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("clear should have argument but is called with 0");
                return 1;
            }
            return 0;
        }

        static private int execute_pwd(string[] cmd)
        {
            if (cmd.Length>1)
            {
                Console.Error.WriteLine("clear should not have argument but is called with some");
                return 1;
            }
            else
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
            }
            return 0;
        }

        static private int execute_clear(string[] cmd)
        {
            if (cmd.Length>1)
            {
                Console.Error.WriteLine("clear should not have argument but is called with some");
                return 1;
            }
            Console.Clear();
            return 0;
        }

        static private int execute_exit(string[] cmd)
        {
            {
                return -1;
            }
        }
    }
}