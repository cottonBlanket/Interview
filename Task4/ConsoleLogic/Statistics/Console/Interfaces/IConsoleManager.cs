namespace Task4.Statistics.Interfaces;

/// <summary>
/// интерфейс обработчика статистики полученной из консоли системы статистики
/// </summary>
public interface IConsoleStatisticManager
{
    /// <summary>
    /// метод обработки консольной команды со статистикой
    /// </summary>
    /// <param name="input">команда для обработки</param>
    Task Execute(string input);
}