
ThreadPool.QueueUserWorkItem(Work);

ThreadPool.QueueUserWorkItem((s) =>
{
    Console.WriteLine($"Task running on Thread: {Environment.CurrentManagedThreadId}");
});

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
Task.Run(() =>
{
    Console.WriteLine($"Task running on Thread: {Environment.CurrentManagedThreadId}");
});
#pragma warning restore CS4014

Console.WriteLine("Main thread is free to do other work...");

ThreadPool.QueueUserWorkItem((s) =>
{
    Console.WriteLine($"Task running on Thread: {Environment.CurrentManagedThreadId}");
});

Thread.Sleep(2000);

// do some work
static void Work(object? state)
{
    Console.WriteLine($"Task running on Thread: {Environment.CurrentManagedThreadId}");
}
