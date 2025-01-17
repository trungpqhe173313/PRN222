namespace Demo01
{
    class Program
    {
        static void PrintNumber(string message)
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"{message}:{i}");
                Thread.Sleep(1000); // Tạm dừng 1 giây
            }
        }

        static void Main()
        {
            Thread.CurrentThread.Name = "Main"; // Đặt tên cho luồng chính

            // Tạo Task bằng biểu thức lambda
            Task task01 = new Task(() => PrintNumber("Task 01"));
            task01.Start();

            // Tạo Task bằng delegate và chạy Task
            Task task02 = Task.Run(delegate { PrintNumber("Task 02"); });

            // Tạo Task bằng Action delegate
            Task task03 = new Task(new Action(() => { PrintNumber("Task 03"); }));
            task03.Start();

            Console.WriteLine($"Thread '{Thread.CurrentThread.Name}'"); // In ra tên thread hiện tại (Main)
            Console.ReadKey(); // Giữ cửa sổ console mở
        }
    }
}
