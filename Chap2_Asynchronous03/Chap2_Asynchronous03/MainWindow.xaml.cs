using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Chap2_Asynchronous03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private readonly HttpClient client = new HttpClient { MaxResponseContentBufferSize = 1_000_000 };

        private readonly IEnumerable<string> UrlList = new string[] {
        "https://docs.microsoft.com",
        "https://docs.microsoft.com/azure",
        "https://docs.microsoft.com/powershell",
        "https://docs.microsoft.com/dotnet",
        "https://docs.microsoft.com/aspnet/core",
        "https://docs.microsoft.com/windows"
    };

        private async void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            btnStartButton.IsEnabled = false; // Vô hiệu hóa nút "Start" khi bắt đầu
            txtResults.Clear(); // Xóa nội dung của textbox

            await SumPageSizesAsync(); // Gọi hàm tính tổng kích thước trang web bất đồng bộ

            txtResults.Text += $"\nControl returned to {nameof(OnStartButtonClick)}."; // In thông báo khi hàm OnStartButtonClick kết thúc
            btnStartButton.IsEnabled = true; // Kích hoạt lại nút "Start"
        }

        private async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew(); // Khởi tạo và bắt đầu đo thời gian
            int total = 0; // Khởi tạo biến total để tính tổng kích thước

            // Duyệt qua danh sách URL
            foreach (string url in UrlList)
            {
                // Gọi hàm ProcessUrlAsync để lấy kích thước nội dung của URL
                int contentLength = await ProcessUrlAsync(url, client);

                // Cộng kích thước nội dung vào biến total
                total += contentLength;
            }

            stopwatch.Stop(); // Dừng đo thời gian

            // In kết quả ra textbox
            txtResults.Text += $"\nTotal bytes returned: {total:#,#}";
            txtResults.Text += $"\nElapsed time: {stopwatch.Elapsed}\n";
        }

        private async Task<int> ProcessUrlAsync(string url, HttpClient client)
        {
            // Tải nội dung của URL bất đồng bộ
            byte[] content = await client.GetByteArrayAsync(url);

            // Gọi hàm DisplayResults để hiển thị kết quả
            DisplayResults(url, content);

            // Trả về kích thước nội dung
            return content.Length;
        }

        private void DisplayResults(string url, byte[] content) =>
            txtResults.Text += $"{url,-60} {content.Length,10:#,#}\n";

        protected override void OnClosed(EventArgs e) => client.Dispose();
    }
}