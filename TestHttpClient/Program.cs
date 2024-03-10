// Top-level statement version for .NET 6 and later, including .NET 8


using TestHttpClient;

if (args.Length > 0)
{
    var instances = int.Parse(args[0]);
    var tasks = new Task[instances];

    for (var i = 0; i < instances; i++)
    {
        var taskNum = i; // Avoid modified closure issue
        tasks[i] = HttpSender.RequestAsync();
    }

    Task.WaitAll(tasks);
}
else
{
    Console.WriteLine("No arguments provided.");
}