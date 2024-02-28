namespace Menu.Interface;

public interface IMenu
{
    /// <summary>
    /// Метод должен считывать нажатия на клавиши и возвращать индекс пункта элемента.
    /// </summary>
    /// <returns>Индекс выбранного пункта.</returns>
    public int RunMenu();

    /// <summary>
    /// Возвращает объект элемента меню
    /// </summary>
    /// <param name="index">Индекс пункта меню.</param>
    /// <returns>Пункт меню.</returns>
    public IMenuItem GetMenuItem(int index);
}
