using System;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace Connect4
{
    public class Network
    {
        public static IPAddress My_ip()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            }
            catch (Exception e)
            {
                Console.WriteLine("erreur My_ip");
                throw;
            }
            
        }

        public static Socket Connect(string ip)
        {
            try
            {
                Socket connect_socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
                connect_socket.Connect(ip, 11000);
                return connect_socket;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
            
        }

        public static Socket Wait_connection()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP = new IPEndPoint(My_ip(), 11000);
            socket.Bind(remoteEP);
            socket.Listen(1);
            return socket.Accept();
        }

        public static bool Send_action(Socket a, Byte action)
        {
            if (a.Connected)
            {
                byte[] buf = {action};
                a.Send(buf);
                return true;
            }
            return false;

        }

        public static byte Receive_action(Socket s)
        {
            byte[] buf = new byte[256];
            s.Receive(buf);
            return buf[0];
        }
    }
}