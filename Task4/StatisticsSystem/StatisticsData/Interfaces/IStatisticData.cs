namespace Task4.Statistics.Interfaces;

/// <summary>
/// интерфейс класса, хранящего собранные данные статистики
/// </summary>
public interface IStatisticData
{
    /// <summary>
    /// добавляет новые значения статистики по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="values">новые значения</param>
    /// <returns>асинхронное действие</returns>
    Task Append(string key, string values);
    
    /// <summary>
    /// очищает данные статистики по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронное действие</returns>
    Task Clear(string key);
    
    /// <summary>
    /// отдает статистику по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронное действие</returns>
    Task Stat(string key);
}