using System;
using System.IO;

namespace Exercise1
{
	internal static class CopyFile
	{
		/// <summary>
		///   EXERCISE 1.3:
		///   <para />
		///   Read the source file and put its content into destination file
		/// </summary>
		/// <param name="source">the path to the source file</param>
		/// <param name="destination">the path to the destination file</param>
		public static void CopyAllFile(string source, string destination)
		{
			try
			{
				string[] line = File.ReadAllLines(source);	

				for (int i = 0; i < line.Length; i++)
				{
					File.AppendAllText(destination, File.ReadAllText(source));
				}
			}
			catch
			{
				Console.WriteLine("file does not exist or wrong accessing path");
				throw;
			}
		}

		/// <summary>
		///   EXERCISE 1.4:
		///   <para />
		///   Read the source file and put half of its content into destination file
		/// </summary>
		/// <param name="source">the path to the source file</param>
		/// <param name="destination">the path to the destination file</param>
		public static void CopyHalfFile(string source, string destination)
		{
			try
			{
				string[] line = File.ReadAllLines(source);
				int half_size = line.Length / 2;
				
				for (int i = 0; i < half_size; i++)
				{
					File.AppendAllText(destination, File.ReadAllText(source));
				}
			}
			catch
			{
				Console.WriteLine("file does not exist or wrong accessing path");
				throw;
			}
		}
	}
}