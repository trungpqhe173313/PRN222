using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    public static void Main()
    {
        var range = Enumerable.Range(1, 1000_000);

        // Phiên bản tuần tự
        var resultList = range.Where(i => i % 3 == 0).ToList();
        Console.WriteLine($"Tuần tự: Tổng số mục là {resultList.Count}");

        // Phiên bản song song sử dụng phương thức AsParallel
        resultList = range.AsParallel().Where(i => i % 3 == 0).ToList();
        Console.WriteLine($"Song song: Tổng số mục là {resultList.Count}");

        resultList = (from i in range.AsParallel()
                      where i % 3 == 0
                      select i).ToList();
        Console.WriteLine($"Song song: Tổng số mục là {resultList.Count}");

        Console.ReadLine();
    }
}