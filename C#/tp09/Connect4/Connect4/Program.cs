using System;
using System.Net.Sockets;

namespace Connect4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Votre adresse ip : <{0}>", Network.My_ip());
            Console.WriteLine("Voulez-vous héberger la partie ? (O/N) :");
            Console.Write(">");
            string network_option = Console.ReadLine();
            
            
            if (network_option == "O")
            {
                Network.Wait_connection();
                Console.WriteLine("Vous êtes de la couleur Rouge");
                //game();
            }
            else
            {
                bool is_ip_valide = false;
                while (!is_ip_valide)
                {
                    Console.WriteLine("A quel adresse IP voulez-vous vous connecter ?");
                    Console.Write(">");
                    string entered_ip = Console.ReadLine();
                    //always working
                    is_ip_valide = true;
                    Network.Connect(entered_ip);
                    
                    Console.WriteLine("Vous êtes de la couleur Jaune");
                }
            }
            //game();
        }

 
        
        static void game(Connect4.state player, Socket s)
        {
            bool ismygridempty = true;
            Connect4 new_connect = new Connect4();
            
            
            /*while (s.Connected && ismygridempty)
            {
                while (new_connect.Check_Win(player))
                {
                    
                }
            }*/
        }
    }
}