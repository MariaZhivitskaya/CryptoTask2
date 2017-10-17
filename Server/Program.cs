using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    class Program
    {
        private Random rand = new Random();

        private enum FileName
        {
            DeepPurple, PinkFloyd, Rammstein, Metallica, WhiteSnake
        }

        static void Main(string[] args)
        {
            TcpListener listner = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12000));
            listner.Start();

            while (true)
            {
                TcpClient client = listner.AcceptTcpClient();

                StreamReader sr = new StreamReader(client.GetStream());
                string RSAkey = sr.ReadLine();

                StreamWriter sw = new StreamWriter(client.GetStream())
                {
                    AutoFlush = true
                };

                Console.WriteLine("Server: RSA key recieved");
                sw.WriteLine("RSA key recieved");

                //Console.WriteLine("Server: sending file names");
                //sw.WriteLine(FileList());

                string filename = sr.ReadLine();
                Console.WriteLine("Server: " + filename);

                Console.WriteLine("Server: sending encrypted text");
                sw.WriteLine(EncryptFile(filename));
                //Console.WriteLine("Server: encrypting text " + fileNameFromClient);
                //Console.WriteLine("Client : " + sr.ReadLine());
                //Console.WriteLine("Server : Пока");
                //sw.WriteLine("Пока");

                client.Close();
            }
        }

        private int GenerateSessionKey()
        {
            return rand.Next();
        }

        //private static string FileList()
        //{
        //    string str = "";

        //    for (int i = 0; i < Enum.GetNames(typeof(FileName)).Length; i++)
        //    {
        //        str += (i + 1) + ": " + ((FileName) i) + "\n";
        //    }

        //    return str;
        //}

        private static string EncryptFile(string filename)
        {
            using (StreamReader streamReader = new StreamReader(filename))
            {
                return streamReader.ReadToEnd().ToUpper();
            }
        }
    }
}
