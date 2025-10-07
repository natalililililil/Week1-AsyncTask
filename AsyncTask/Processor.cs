using System.Diagnostics;

namespace AsyncTask
{
    internal static class Processor
    {
        private static readonly string[] Files = { "Файл 1", "Файл 2", "Файл 3" };

        internal static void RunSync()
        {
            var timer = Stopwatch.StartNew();

            foreach (var file in Files)
                ProcessData(file);

            timer.Stop();

            Console.WriteLine($"Общее время выполнения синхронной задачи: {timer.ElapsedMilliseconds} мс");
        }

        internal static async Task RunAsync()
        {
            var timer = Stopwatch.StartNew();

            var tasks = Files.Select(ProcessDataAsync).ToList();

            while (tasks.Count > 0)
            {
                var finished = await Task.WhenAny(tasks);
                tasks.Remove(finished);
            }

            timer.Stop();

            Console.WriteLine($"Общее время выполнения асинхронной задачи: {timer.ElapsedMilliseconds} мс");
        }

        private static void ProcessData(string dataName)
        {
            Thread.Sleep(3000);
            Console.WriteLine($"Обработка '{dataName}' завершена за 3 секунды");
        }

        private static async Task ProcessDataAsync(string dataName)
        {
            await Task.Delay(3000);
            Console.WriteLine($"Обработка '{dataName}' завершена за 3 секунды");
        }
    }
}
