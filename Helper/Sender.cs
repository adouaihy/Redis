using System;
using System.Net.Sockets;

namespace Helper
{
    public static class Sender
    {
        public static string Send(String server, int port, String message)
        {
            String responseData = String.Empty;
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    using (NetworkStream stream = client.GetStream())
                    {
                        stream.Write(data, 0, data.Length);
                        data = new Byte[256];

                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        stream.Close();
                    }
                    client.Close();
                }
                return responseData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return null;
            }
        }
    }
}
