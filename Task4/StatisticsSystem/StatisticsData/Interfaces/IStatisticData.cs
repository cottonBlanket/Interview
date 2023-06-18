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
    void Append(string key, IEnumerable<int> values);
    
    /// <summary>
    /// очищает данные статистики по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    void Clear(string key);
    
    /// <summary>
    /// отдает статистику по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    void Stat(string key);
}