using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    // Hàm IsPrime trả về true nếu số là số nguyên tố, ngược lại trả về false.
    private static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }
        if (number == 2)
        {
            return true;
        }
        if (number % 2 == 0)
        {
            return false;
        }
        for (int divisor = 3; divisor <= Math.Sqrt(number); divisor += 2)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }
        return true;
    }

    // GetPrimeList trả về danh sách số nguyên tố bằng cách sử dụng ForEach tuần tự
    private static IList<int> GetPrimeList(IList<int> numbers) => numbers.Where(IsPrime).ToList();

    // GetPrimeListWithParallel trả về danh sách số nguyên tố bằng cách sử dụng Parallel.ForEach
    private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
    {
        var primeNumbers = new ConcurrentBag<int>(); // ConcurrentBag<int> được bôi đỏ
        Parallel.ForEach(numbers, number => { // Parallel.ForEach được bôi đỏ
            if (IsPrime(number))
            {
                primeNumbers.Add(number); // primeNumbers.Add(number) được bôi đỏ
            }
        });
        return primeNumbers.ToList();
    }

    static void Main()
    {
        // 2 triệu
        var limit = 2_000_000; // 2_000_000 được bôi đỏ
        var numbers = Enumerable.Range(0, limit).ToList(); // Enumerable.Range(0, limit).ToList() được bôi đỏ

        var watch = Stopwatch.StartNew();
        var primeNumbersFromForeach = GetPrimeList(numbers);
        watch.Stop();

        var watchForParallel = Stopwatch.StartNew();
        var primeNumbersFromParallelForeach = GetPrimeListWithParallel(numbers);
        watchForParallel.Stop();

        Console.WriteLine($"Classical foreach loop | Total prime numbers: {primeNumbersFromForeach.Count} | Time Taken: {watch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Parallel.ForEach loop | Total prime numbers: {primeNumbersFromParallelForeach.Count} | Time Taken: {watchForParallel.ElapsedMilliseconds} ms.");

        Console.WriteLine("Press any key to exit.");
        Console.ReadLine();

    }//end Main
}//end Program
    }
}