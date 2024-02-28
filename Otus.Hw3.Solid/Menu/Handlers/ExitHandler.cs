using System;
using Menu.Interface;

namespace Menu.Handler;

public class ExitHandler : IHandler
{
    /// <inheritdoc/>
    public void Run()
    {
        Console.WriteLine("Выходим...");
        Environment.Exit(1);
    }
}
