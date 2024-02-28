using System;
using System.Collections.Generic;
using Menu;
using Menu.Handler;
using Menu.Helpers;
using Menu.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Otus.Hw3.Solid.Interfaces;
using Otus.Hw3.Solid.Models;
using Otus.Hw3.Solid.Services;

namespace Otus.Hw3.Solid;

public class App
{
    private readonly ITestService _testService;
    private readonly ILogger<App> _logger;
    private readonly AppSettings _config;
    private readonly IGame _game;

    public App(ITestService testService,
        IOptions<AppSettings> config,
        ILogger<App> logger,
        IGame game)
    {
        _testService = testService;
        _logger = logger;
        _config = config.Value;
        _game = game;
    }

    public void Run()
    {
        ShowTopInfo();

        IMenu menu = new MenuHandler(GetMenuItems(), Console.CursorTop + 1, 0);

        var cursorTop = 0;

        while (true)
        {
            var selection = menu.RunMenu();

            // очищаем информативную часть консоли
            ConsoleHelper.ClearConsoleArea(Console.CursorTop, cursorTop + 1);

            try
            {
                menu.GetMenuItem(selection).Handler.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            // запоминаем текущее положение курсора для дальнейшей очистки консоли
            cursorTop = Console.CursorTop;
        }
    }

    /// <summary>
    /// Возвращает пункты меню.
    /// </summary>
    /// <returns></returns>
    private IEnumerable<IMenuItem> GetMenuItems()
    {
        var item1 = new MenuItem
        (
            "Играть",
            new GameHandler(_game)
        );

        var item6 = new MenuItem
        (
            "Выход",
            new ExitHandler()
        );

        IMenuItem[] menuItems = { item1, item6 };

        return menuItems;
    }

    /// <summary>
    /// Выводит основную информацию.
    /// </summary>
    private static void ShowTopInfo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Используйте кнопки вверх и вниз для навигации по меню и Enter для выбора.\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("На примере реализации игры «Угадай число» продемонстрировать практическое применение SOLID принципов.");
        Console.WriteLine("Программа рандомно генерирует число, пользователь должен угадать это число.");
        Console.WriteLine("При каждом вводе числа программа пишет больше или меньше отгадываемого.");
        Console.WriteLine("Кол-во попыток отгадывания и диапазон чисел должен задаваться из настроек.");
        Console.WriteLine("В отчёте написать, что именно сделано по каждому принципу.");
    }
}