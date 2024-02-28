namespace Menu.Interface;

public interface IMenuItem
{
    /// <summary>
    /// Заголовок пункта меню.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Обработчик пункта меню.
    /// </summary>
    public IHandler Handler { get; }
}