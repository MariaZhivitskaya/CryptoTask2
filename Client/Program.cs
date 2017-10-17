using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace Client
{
    class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12000));

            StreamWriter sw = new StreamWriter(client.GetStream())
            {
                AutoFlush = true
            };

            Console.WriteLine("Client: Sending RSA Key to server");
            sw.WriteLine(GenerateRSAKey());

            StreamReader sr = new StreamReader(client.GetStream());
            Console.WriteLine("Server : " + sr.ReadLine());

            //Console.WriteLine("-- Input number of file");
            //string fileNumber = Console.ReadLine();

            Console.WriteLine("Client: Sending filename");
            sw.WriteLine("PinkFloyd.txt");

            Console.WriteLine("Server: " + sr.ReadLine());
            //Console.WriteLine("Client : Пока");
            //sw.WriteLine("Пока");
            //Console.WriteLine("Server : " + sr.ReadLine());
            //client.Close();

            Console.ReadKey();
        }

        private static string GenerateRSAKey()
        {
            RSA rsa = new RSACryptoServiceProvider(1024);
            return rsa.ToXmlString(false);
        }
    }
}
