namespace Demo03
{
    class Program
    {
        private static double DoComputation(double start)
        {
            double sum = 0;
            for (double value = start; value <= start + 10; value += 0.1) // Sửa lỗi ở đây: value += 0.1
            {
                sum += value;
            }
            return sum;
        }

        public static void Main()
        {
            // Tạo một mảng Task<double> để lưu trữ 3 Task
            Task<double>[] taskArray = {
            Task<double>.Factory.StartNew(() => DoComputation(1.0)),
            Task<double>.Factory.StartNew(() => DoComputation(100.0)),
            Task<double>.Factory.StartNew(() => DoComputation(1000.0))
        };

            var results = new double[taskArray.Length];
            double sum = 0;

            for (int i = 0; i < taskArray.Length; i++)
            {
                results[i] = taskArray[i].Result; // Lấy kết quả từ Task (chờ Task hoàn thành nếu cần)
                Console.Write("{0:N1} {1}", results[i], i < taskArray.Length - 1 ? "+" : "="); // Định dạng in ra
                sum += results[i];
            }

            Console.WriteLine("{0:N1}", sum);
            Console.ReadKey(); // Giữ cửa sổ console mở
        }
    }
}
