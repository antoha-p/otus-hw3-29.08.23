using System;
using Menu.Interface;
using Otus.Hw3.Solid.Interfaces;

namespace Menu.Handler;

public class GameHandler : IHandler
{
    private readonly IGame _game;

    public GameHandler(IGame game)
    {
        _game = game;
    }

    /// <inheritdoc/>
    public void Run()
    {
        Console.WriteLine("Играем...");
        _game.Start();
        //Environment.Exit(1);
    }
}
