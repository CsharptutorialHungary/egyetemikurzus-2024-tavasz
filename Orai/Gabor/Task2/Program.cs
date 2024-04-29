using System.Runtime.InteropServices;
using System.Text.Json;

namespace Task2
{
    internal class Program
    {
        private static CancellationTokenSource? cts;

        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += OnCancel; //Observer
            Console.WriteLine("Hello, World!");
            cts = new CancellationTokenSource();
            try
            {
                await DoSomethingLong(cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("End of program");

            string s = await File.ReadAllTextAsync(@"c:\asdf.txt");
        }

        private static void OnCancel(object? sender, ConsoleCancelEventArgs e)
        {
            cts?.Cancel();
            e.Cancel = true;
        }

        public static async Task DoSomethingLong(CancellationToken token)
        {
            await Task.Delay(1_000_000, token);
            /*for (int t = 0; t < 1_000_000; t++)
            {
                if (token.IsCancellationRequested)
                    break;

                await Task.Delay(t);
            }*/

            Console.WriteLine("Done");
        }
    }
}
