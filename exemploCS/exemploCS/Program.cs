using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace exemploCS
{

    class Program
    {
        static void Main(string[] args)
        {
            byte stx = 0x02;
            byte etx = 0x03;
            string rs = "|";
            string str;
            
            str = (char)stx + "0199I" + rs + "R010101" + rs + "NOME HOSPEDE" + rs + "D202206221200" + rs + "O202206231300" + (char)etx;
            //str = (char)stx + "0199E" + (char)etx;

            Connect("127.0.0.1", 1024, str);

            Console.WriteLine("Enter para continuar...");
            Console.ReadLine();
        }

        static void Connect(String server, Int32 port, String message)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                data = new Byte[256];

                String responseData = String.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
