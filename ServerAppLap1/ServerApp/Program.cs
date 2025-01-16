using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ServerApp
{
    static void Main(string[] args)
    {
        const int port = 8080;
        TcpListener server = new TcpListener(IPAddress.Any, port);

        try
        {
            server.Start();
            Console.WriteLine($"Server is running on port {port}. Waiting for connections...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {receivedData}");

                string response = receivedData.ToUpper();
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                Console.WriteLine($"Sent: {response}");

                stream.Close();
                client.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            server.Stop();
            Console.WriteLine("Server stopped.");
        }
    }
}
