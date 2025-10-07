using System.Diagnostics;

namespace AsyncTask
{
    internal static class Processor
    {
        private static readonly string[] Files = { "Файл 1", "Файл 2", "Файл 3" };

        internal static void RunSync()
        {
            MeasureTime(() =>
            {
                foreach (var file in Files)
                    ProcessData(file);
            });
        }

        internal static async Task RunAsync()
        {
            await MeasureTimeAsync(async () =>
            {
                var tasks = Files.Select(ProcessDataAsync).ToList();

                while (tasks.Count > 0)
                {
                    var finished = await Task.WhenAny(tasks);
                    tasks.Remove(finished);
                }
            });
        }

        private static void MeasureTime(Action action)
        {
            var timer = Stopwatch.StartNew();
            action();
            timer.Stop();

            Console.WriteLine($"Общее время выполнения синхронной задачи: {timer.ElapsedMilliseconds} мс");
        }

        private static async Task MeasureTimeAsync(Func<Task> asyncAction)
        {
            var timer = Stopwatch.StartNew();
            await asyncAction();
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