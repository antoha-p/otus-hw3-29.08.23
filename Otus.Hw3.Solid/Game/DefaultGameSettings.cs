using Otus.Hw3.Solid.Interfaces;

namespace Otus.Hw3.Solid.Game;

public class DefaultGameSettings : IGameSettings
{
    public int GetMaxNumber()
    {
        return 9;
    }

    public int GetMinNumber()
    {
        return 0;
    }

    public int GetTryNumber()
    {
        return 3;
    }

    public string GetExitWord()
    {
        return "exit";
    }
}