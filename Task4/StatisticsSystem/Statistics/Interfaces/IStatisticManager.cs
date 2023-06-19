namespace Task4.Statistics.Interfaces;

/// <summary>
/// интерфейс обработчика статистики
/// </summary>
public interface IStatisticManager
{
    /// <summary>
    /// метод обработки статистики
    /// </summary>
    /// <param name="input">команда для обработки</param>
    void Execute(string input);
}