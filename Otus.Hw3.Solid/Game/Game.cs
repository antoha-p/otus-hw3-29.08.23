using System;
using Otus.Hw3.Solid.Interfaces;

namespace Otus.Hw3.Solid.Game;

public class Game : IGame
{
    private static readonly Random Random = new Random();

    private readonly IGameSettings _settings;
    private readonly IDisplay _display;

    private int _tryCount;
    private int _minNumber;
    private int _maxNumber;
    private string _exitWord;

    public Game(IGameSettings settings, IDisplay display)
    {
        _settings = settings;
        _display = display;
    }

    public int Start()
    {
        Configure();

        while (true)
        {
            var rand = _minNumber + Random.Next(_maxNumber - _minNumber);

            _display.Show($"New number ({rand})!");

            var isExitNeeded = false;

            for (int i = 0; i < _tryCount; i++)
            {
                _display.Show("Enter number (and press Enter): ");

                var userNumber = _display.Read();

                if (userNumber == _exitWord)
                {
                    isExitNeeded = true;
                    break;
                }

                if (IsWin(int.Parse(userNumber), rand))
                {
                    isExitNeeded = true;
                    break;
                }
            }

            if (isExitNeeded)
            {
                break;
            }
        }

        return 0;
    }

    private void Configure()
    {
        _tryCount = _settings.GetTryNumber();
        _minNumber = _settings.GetMinNumber();
        _maxNumber = _settings.GetMaxNumber();
        _exitWord = _settings.GetExitWord();
    }

    private bool IsWin(int a, int rand)
    {
        if (a < rand)
        {
            _display.Show("More!");
            return false;
        }

        if (a > rand)
        {
            _display.Show("Less!");
            return false;
        }

        _display.Show("Win!");
        return true;
    }
}