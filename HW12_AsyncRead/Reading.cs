using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12_AsyncRead
{
    internal class Reading
    {
        public static async Task Run(string[] paths, CancellationToken token)
        {
            var res = new List<string>();
            var tasks = new List<Task<int>>();
            foreach (var path in paths)
            {
                tasks.Add(ReadAsync(path, token));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            finally
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].IsCanceled)
                    {
                        res.Add("tasks was Canceled");
                        break;
                    }

                    if (tasks[i].IsCompleted || tasks[i].IsFaulted)
                    {
                        res.Add($"in file {i + 1} was {tasks[i].Result} sympbols");
                    }
                }

                foreach (var mes in res)
                {
                    Console.WriteLine(mes);
                }
            }
        }


        static async Task<int> ReadAsync(string path, CancellationToken token)
        {
            int totalReadSymbols = 0, readSyblos = 0;
            Char[] buffer = new Char[50];
            using (var sr = new StreamReader(path))
            {
                while (true)
                {
                    readSyblos = sr.ReadBlock(buffer, 0, 50);
                    totalReadSymbols += readSyblos;
                    if (readSyblos == 0) break;
                    if (totalReadSymbols == 0) throw new IOException();
                    token.ThrowIfCancellationRequested();
                    //Console.WriteLine(buffer[..readSyblos]);
                    await Task.Delay(1000, token);
                }
            }

            return totalReadSymbols;
        }

    }
}
