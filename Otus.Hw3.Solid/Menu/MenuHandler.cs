using System;
using System.Collections.Generic;
using System.Linq;
using Menu.Interface;
using static System.Console;

namespace Menu;

public class MenuHandler : IMenu
{
    private readonly List<IMenuItem> _menuList;
    private int _currentSelection;
    private int _drawMenuColumnPos;
    private readonly int _drawMenuRowPos;
    private int _menuMaximumWidth;

    public MenuHandler(IEnumerable<IMenuItem> options, int row, int col)
    {
        _menuList = options.ToList();
        _currentSelection = 0;
        _drawMenuRowPos = row;
        _drawMenuColumnPos = col;

        Configure();
    }

    /// <inheritdoc/>
    public IMenuItem GetMenuItem(int index)
    {
        return _menuList[index];
    }

    /// <summary>
    /// Конфигурация меню.
    /// </summary>
    public void Configure()
    {
        ModifyMenuCentered();
        CenterMenuToConsole();
        ResetCursorVisible();
    }

    /// <summary>
    /// Центрует меню в консоли.
    /// </summary>
    private void CenterMenuToConsole()
    {
        _drawMenuColumnPos = GetConsoleWindowWidth() / 2 - _menuMaximumWidth / 2;
    }

    /// <summary>
    /// Выравнивает по левому краю меню.
    /// </summary>
    public void ModifyMenuLeftJustified()
    {
        var space = "";

        var maximumWidth = _menuList.Select(t => t.Title.Length).Prepend(0).Max();

        maximumWidth += 6;

        foreach (var menuItem in _menuList)
        {
            int spacesToAdd = maximumWidth - menuItem.Title.Length;

            for (int j = 0; j < spacesToAdd; j++)
            {
                space += " ";
            }

            menuItem.Title += space;
            space = "";
        }

        _menuMaximumWidth = maximumWidth;
    }

    /// <summary>
    /// Центрует пункты меню.
    /// </summary>
    private void ModifyMenuCentered()
    {
        var space = "";

        var maximumWidth = _menuList.Select(t => t.Title.Length).Prepend(0).Max();

        maximumWidth += 6;

        foreach (var menuItem in _menuList)
        {
            var minimumWidth = (maximumWidth - menuItem.Title.Length) / 2;

            for (var j = 0; j < minimumWidth; j++)
            {
                space += " ";
            }

            menuItem.Title = space + menuItem.Title + space;
            space = "";
        }

        foreach (var menuItem in _menuList.Where(t => t.Title.Length < maximumWidth))
        {
            menuItem.Title += " ";
        }

        _menuMaximumWidth = maximumWidth;
    }

    /// <summary>
    /// Возвращает ширину консоли.
    /// </summary>
    /// <returns>Ширина консоли.</returns>
    private static int GetConsoleWindowWidth()
    {
        return WindowWidth;
    }

    /// <summary>
    /// Устанавливает цвет текста и фона.
    /// </summary>
    /// <param name="foreground">Цвет текста.</param>
    /// <param name="background">Цвет фона.</param>
    private static void SetConsoleTextColor(ConsoleColor foreground, ConsoleColor background)
    {
        ForegroundColor = foreground;
        BackgroundColor = background;
    }

    /// <summary>
    /// Выключает мигание курсора.
    /// </summary>
    private static void ResetCursorVisible()
    {
        CursorVisible = CursorVisible != true;
    }

    /// <summary>
    /// Устанавливает позицию курсора.
    /// </summary>
    /// <param name="row">Отступ сверху.</param>
    /// <param name="column">Отступ слева.</param>
    private static void SetCursorPosition(int row, int column)
    {
        if (row > 0 && row < WindowHeight)
        {
            CursorTop = row;
        }

        if (column > 0 && column < WindowWidth)
        {
            CursorLeft = column;
        }
    }

    /// <inheritdoc/>
    public int RunMenu()
    {
        var run = true;
        DrawMenu();

        while (run)
        {
            var keyInfo = ReadKey(true);
            var key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    _currentSelection--;

                    if (_currentSelection < 0)
                    {
                        _currentSelection = _menuList.Count - 1;
                    }

                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    _currentSelection++;

                    if (_currentSelection > _menuList.Count - 1)
                    {
                        _currentSelection = 0;
                    }

                    break;
                }
                case ConsoleKey.Enter:
                    run = false;
                    break;
                case ConsoleKey.Q:
                    Environment.Exit(1);
                    break;
            }

            DrawMenu();
        }

        return _currentSelection;
    }

    /// <summary>
    /// Выводит меню в консоль.
    /// </summary>
    private void DrawMenu()
    {
        for (var i = 0; i < _menuList.Count; i++)
        {
            SetCursorPosition(_drawMenuRowPos + i, _drawMenuColumnPos);
            SetConsoleTextColor(ConsoleColor.White, ConsoleColor.Black);

            if (i == _currentSelection)
            {
                SetConsoleTextColor(ConsoleColor.Black, ConsoleColor.White);
            }

            WriteLine(_menuList[i].Title);
            ResetColor();
        }
    }
}
