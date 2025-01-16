﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
class Program
{
    static void ProcessMessaage(object parm)
    {
        string data;
        int count;
        try
        {
            TcpClient client = parm as TcpClient;
            Byte[] bytes = new byte[256];
            NetworkStream stream = client.GetStream();
            while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                Console.WriteLine($"Received: {data} at {DateTime.Now: t}");
                data = $"{data.ToUpper()}";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine($"Sent: {data}");
            }
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0}", ex.Message);
            Console.WriteLine("Waiting message ...");
        }
    }
    static void ExecuteServer(string host, int port)
    {
        int Count = 0;
        TcpListener server = null;
        try
        {
            Console.Title = "Server Application";
            IPAddress localAddr = IPAddress.Parse(host);
            server = new TcpListener(localAddr, port);

            server.Start();
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Waiting for a connection ...");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"Number of client connected: {++Count}");
                Console.WriteLine(new string('*', 40));
                Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessaage));
                thread.Start(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: {0}", ex.Message);
        }
        finally
        {
            server.Stop();
            Console.WriteLine("Server stopped. Press any key to exit !");
        }
        Console.Read();
    }
    public static void main()
    {
        string host = "127.0.0.1";
        int port = 13000;
        ExecuteServer(host, port);
    }
}