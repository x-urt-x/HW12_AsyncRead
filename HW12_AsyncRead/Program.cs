using System.Threading;

namespace HW12_AsyncRead
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var paths = new[] { "file1.txt", "file2.txt", "file3.txt", "file4.txt", "file5.txt" };
            Task.Run(() => Reading.Run(paths, token));
            if(Console.ReadLine()== "Interrup") cts.Cancel();
            Console.ReadLine();
        }

    }
}