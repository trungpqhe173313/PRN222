namespace Demo02
{
    class Program
    {
        public static void Main()
        {
            // Tạo một mảng Task để lưu trữ 5 Task
            Task[] tasks = new Task[5];
            String taskData = "Hello";

            // Tạo 5 Task và gán vào mảng
            for (int i = 0; i < 5; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    // In ra thông tin về Task và Thread đang thực thi
                    Console.WriteLine($"Task={Task.CurrentId}, obj={taskData}, ThreadId={Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(1000); // Tạm dừng 1 giây
                });
            }

            try
            {
                // Chờ cho tất cả các Task trong mảng hoàn thành
                Task.WaitAll(tasks);
            }
            catch (AggregateException ae)
            {
                // Xử lý các ngoại lệ nếu có bất kỳ Task nào bị lỗi
                Console.WriteLine("One or more exceptions occurred: ");
                foreach (var ex in ae.Flatten().InnerExceptions)
                    Console.WriteLine($" {0}", ex.Message);
            }

            // In ra trạng thái của từng Task sau khi tất cả đã hoàn thành
            Console.WriteLine("Status of completed tasks:");
            foreach (var t in tasks)
            {
                Console.WriteLine($" Task #{t.Id}: {t.Status}");
            }

            Console.ReadKey(); // Giữ cửa sổ console mở
        }
    }
}
