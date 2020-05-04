using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Brainfuck
{
    class Program
    {
        [STAThread]
        public static Dictionary<char, char> classicBrainfuck()
        {
            Dictionary<char, char> brainfuck = new Dictionary<char, char>();
            brainfuck.Add('>', '>');
            brainfuck.Add('<', '<');
            brainfuck.Add('+', '+');
            brainfuck.Add('-', '-');
            brainfuck.Add('.', '.');
            brainfuck.Add(',', ',');
            brainfuck.Add('[', '[');
            brainfuck.Add(']', ']');
            
            return brainfuck;
        }
        
        public static void Main(string[] args)
        {
            /* Ce code permet de lancer l'interface graphique pour tester vos fonctions.
               Vous pouvez aussi vous rediger des tests en console... */
             
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new IDE());
           
        }

    }
}
