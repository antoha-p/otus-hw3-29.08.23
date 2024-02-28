using System;

namespace Menu.Helpers;

public class ConsoleHelper
{
    /// <summary>
    /// Очищает область консоли и устанавливает обратно позицию курсора.
    /// </summary>
    public static void ClearConsoleArea(int positionFrom, int positionTo)
    {
        var currentCursorTop = Console.CursorTop;

        for (var i = positionFrom; i < positionTo + 1; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        Console.SetCursorPosition(0, currentCursorTop);
    }
}