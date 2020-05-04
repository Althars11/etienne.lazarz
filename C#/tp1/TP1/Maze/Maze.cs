using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security.Principal;
using Microsoft;
using Microsoft.Win32.SafeHandles;

namespace Maze
{
    internal static class Maze
    {
        /// <summary>
        /// MAZE:
        /// <para/>
        /// This is where you should call your functions to make your program work.
        /// </summary>
        /// <param name="args">unused</param>
        public static void Main(string[] args)
        {
            // AskmazeFile
            // get .solved filename
            // parse .maze file
            // print the maze (BONUS)
            // solve the maze
            // print the maze (BONUS)
            // save solution in .solved file
            string path = AskMazeFile();
            string outputfile = GetOutPutFile(path);
            char[][] maze = ParseFile(path);
            Point s = FindStart(maze);
            
            int[][] processed = new int[maze.Length][];
            
            for (int i = 0; i < maze.Length; i++)
            {
                processed[i] = new int[maze[0].Length];
                for (int j = 0; j < maze[0].Length; j++)
                {
                    processed[i][j] = 0;
                }
            }
            Console.WriteLine(SolveMazeBackTracking(maze, processed, s, outputfile));
        }

        public static string AskMazeFile()
        {
            Console.WriteLine("> Which file should be loaded ?");
            Console.Write(">");
            string path = Console.ReadLine();
            if (Path.GetExtension(path) == ".maze")
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("Thank you, bye !");
                }
                else
                {
                    Console.Error.WriteLine("le fichier n'existe pas ou le chemin est invalide");
                    Console.ReadLine();
                    Console.Clear();
                    AskMazeFile();
                }
            }
            else
            {
                Console.Error.WriteLine("ce n'est pas un .maze");
                Console.ReadLine();
                Console.Clear();
                AskMazeFile();
            }

            return path;
        }

        public static string GetOutPutFile(string fileIn)
        {
            return Path.ChangeExtension(fileIn, ".solved");
        }

        public static char[][] ParseFile(string file)
        {
            string[] fileline = File.ReadAllLines(file);

            int nbline = fileline.Length;
            int lineLen = fileline[0].Length;

            char[][] maze = new char[nbline][];

            for (int i = 0; i < nbline; i++)
            {
                maze[i] = new char[lineLen];
                string line = fileline[i];
                for (int j = 0; j < lineLen; j++)
                {
                    maze[i][j] = line[j];
                }
            }

            return maze;
        }

        private static Point FindStart(char[][] grid)
        {
            bool is_start = false;
            int i = 0;
            int gridLen = grid[0].Length;
            int lineLen = grid[1].Length;
            Point s = new Point(0, 0);

            while ((i < gridLen) & (is_start == false))
            {
                int j = 0;
                while ((j < lineLen) & (is_start == false))
                {
                    if (grid[i][j] == 'S')
                    {
                        s = new Point(i, j);
                        is_start = true;
                    }
                    else
                    {
                        j++;
                    }
                }

                i++;
            }

            return s;
        }

        public static bool SolveMazeBackTracking(char[][] grid, int[][] processed, Point p, string outputfile)
        {
            int i = p.X;
            int j = p.Y;
            if (((i < 0) || (i > grid.Length-1)) || (j < 0 || j > grid[0].Length-1))
            {
                return false;
            }
            else
            {
                if (processed[i][j] == 'A') //A stand for "already went there"
                {
                    return false;
                }
                else
                {
                    processed[i][j] = 'A';
                    if (grid[i][j] == 'F')
                    {
                        grid[i][j] = 'P';
                        SaveSolution(grid, outputfile);
                        return true;
                    }
                    else
                    {
                        if (grid[i][j] == 'B')
                        {
                            return false;
                        }
                        else
                        {
                            if (SolveMazeBackTracking(grid, processed, p = new Point(i + 1, j), outputfile)==true)
                            {
                                grid[p.X][p.Y] = 'P';
                                SaveSolution(grid, outputfile);
                                
                                return true;
                            }
                            else
                            {
                                if (SolveMazeBackTracking(grid, processed, p = new Point(i, j + 1), outputfile)==true)
                                {
                                    grid[p.X][p.Y] = 'P';
                                    SaveSolution(grid, outputfile);
                                    return true;
                                }
                                else
                                {
                                    if (SolveMazeBackTracking(grid, processed, p = new Point(i, j - 1), outputfile))
                                    {
                                        grid[p.X][p.Y] = 'P';
                                        SaveSolution(grid, outputfile);
                                        return true;
                                    }
                                    else
                                    {
                                        if (SolveMazeBackTracking(grid, processed, p = new Point(i - 1, j), outputfile))
                                        {
                                            
                                            grid[p.X][p.Y] = 'P';
                                            SaveSolution(grid, outputfile);
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static void SaveSolution(char[][] grid, string fileOut)
        {
            string pline = "";
            for (int j = 0; j < grid[0].Length; j++)
            {
                pline = pline + grid[0][j];
            }

            File.WriteAllText(fileOut, pline + "\n");

            for (int i = 1; i < grid.Length; i++)
            {
                string line = "";
                for (int j = 0; j < grid[0].Length; j++)
                {
                    line = line + grid[i][j];
                }

                File.AppendAllText(fileOut, line + "\n");
            }
        }
    }

    /// <summary>
    ///   Class that allows to store 2D coordinates.
    /// </summary>
    internal class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Y { get; set; }
        public int X { get; set; }
    }
}
