using System;
using Otus.Hw3.Solid.Interfaces;

namespace Otus.Hw3.Solid.Services;

public class ConsoleDisplay : IDisplay
{
    public int Show(string text)
    {
        Console.Out.WriteLine(text);
        return text.Length;
    }

    public string Read()
    {
        return Console.ReadLine();
    }
}