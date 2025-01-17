namespace Demo04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for the first 10 tasks to complete...\n");

            var tasks = new List<Task<int>>();
            var source = new CancellationTokenSource();
            var token = source.Token;
            int completedIterations = 0;

            for (int n = 1; n <= 20; n++)
            {
                tasks.Add(Task.Run(() =>
                {
                    int iterations = 0;
                    for (int ctr = 1; ctr <= 2_000_000; ctr++)
                    {
                        token.ThrowIfCancellationRequested(); // Kiểm tra xem Task có bị hủy hay không
                        iterations++;
                    }

                    Interlocked.Increment(ref completedIterations); // Tăng biến đếm số Task đã hoàn thành
                    if (completedIterations >= 10)
                    {
                        source.Cancel(); // Hủy các Task còn lại khi 10 Task đầu tiên hoàn thành
                    }
                    return iterations;
                }, token)); // Truyền token vào Task
            }

            try
            {
                Task.WaitAll(tasks.ToArray()); // Chờ tất cả các Task hoàn thành
            }
            catch (AggregateException)
            {
                Console.WriteLine("Status of tasks: \n");
                Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id", "Status", "Iterations");
                foreach (var t in tasks)
                {
                    Console.WriteLine("{0,10} {1,20} {2,14}",
                        t.Id, t.Status,
                        t.Status == TaskStatus.Canceled ? "n/a" : t.Result.ToString("N0")); // In kết quả hoặc "n/a" nếu bị hủy
                }
            }
            Console.ReadLine();
        }
    }
}
