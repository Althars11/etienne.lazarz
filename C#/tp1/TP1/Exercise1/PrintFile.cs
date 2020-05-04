using System;
using System.IO;

namespace Exercise1
{
	internal static class PrintFile
	{
		/// <summary>
		///   EXERCISE 1.1:
		///   <para />
		///   Read all the text in a file and print it in the console.
		/// </summary>
		/// <param name="path">the path (absolute or relative) to the file</param>
		public static void PrintAllFile(string path)
		{
			if (File.Exists(path))
			{
				string[] line = File.ReadAllLines(path);
				for (int j = 0; j < line.Length; j++)
				{
					Console.WriteLine(line[j]);
				}
			}
			else
			{
				Console.Error.WriteLine("file does not exist or wrong accessing path");
			}
		}

		/// <summary>
		///   EXERCISE 1.2:
		///   <para />
		///   Read only one line out of two and print it in the console.
		/// </summary>
		/// <param name="path"></param>
		public static void PrintHalfFile(string path)
		{
			if (File.Exists(path))
			{
				string[] line = File.ReadAllLines(path);
				for (int j = 0; j < line.Length; j+=2)
				{
					Console.WriteLine(line[j]);
				}
			}
			else
			{
				Console.Error.WriteLine("file does not exist or wrong accessing path");
			}
		}
	}
}