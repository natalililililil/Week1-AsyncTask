using AsyncTask;

Console.WriteLine("Синхронная работа:");
Processor.RunSync();

Console.WriteLine("\nАсинхронная работа:");
await Processor.RunAsync();

