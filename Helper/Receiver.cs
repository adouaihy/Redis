using System;
using System.Net;
using System.Net.Sockets;

namespace Helper
{
    public class Receiver
    {
        public static void Listen(int port, Func<string, object> onCommandReceived)
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();

                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    data = null;
                    NetworkStream stream = client.GetStream();

                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        object result = onCommandReceived(data.ToString());
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(result.ToString());
                        stream.Write(msg, 0, msg.Length);
                    }

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
