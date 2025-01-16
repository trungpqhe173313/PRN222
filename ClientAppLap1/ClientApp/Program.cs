using System;
using System.Net.Sockets;
using System.Text;

class ClientApp
{
    static void Main(string[] args)
    {
        const string serverAddress = "127.0.0.1"; // Địa chỉ server (localhost)
        const int port = 8080; // Cổng server

        try
        {
            TcpClient client = new TcpClient();
            Console.WriteLine("Connecting to server...");
            client.Connect(serverAddress, port);
            Console.WriteLine("Connected to server.");

            NetworkStream stream = client.GetStream();

            Console.Write("Enter a message: ");
            string message = Console.ReadLine();
            byte[] dataToSend = Encoding.UTF8.GetBytes(message);
            stream.Write(dataToSend, 0, dataToSend.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Response from server: {response}");

            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
