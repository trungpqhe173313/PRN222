using System;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        // Sử dụng Event-Based Asynchronous Pattern (EAP)
        DownloadAsynchronously();

        Console.WriteLine("Main thread: Done"); // In ra luồng chính đã hoàn thành
        Console.WriteLine(new string('*', 30)); // In ra một dòng dấu * để phân cách
        Console.ReadLine(); // Giữ cho cửa sổ console mở cho đến khi người dùng nhấn phím
    }

    private static void DownloadAsynchronously()
    {
        WebClient client = new WebClient(); // Tạo một đối tượng WebClient

        // Đăng ký sự kiện DownloadStringCompleted
        client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadComplete);

        // Tải xuống dữ liệu bất đồng bộ từ địa chỉ URI
        client.DownloadStringAsync(new Uri("http://www.aspnet.com"));
    }

    private static void DownloadComplete(object sender, DownloadStringCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Console.WriteLine("Some error has occured."); // In ra thông báo lỗi nếu có
            return;
        }

        // In kết quả
        Console.WriteLine(e.Result); // In ra nội dung của trang web
        Console.WriteLine(new string('*', 30)); // In ra một dòng dấu * để phân cách
        Console.WriteLine("Download completed."); // In ra thông báo tải xuống hoàn thành
    }
}