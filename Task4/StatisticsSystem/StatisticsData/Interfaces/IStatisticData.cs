namespace Task4.Statistics.Interfaces;

/// <summary>
/// интерфейс для работы с хранилищем статистики
/// </summary>
public interface IStatisticData
{
    /// <summary>
    /// добавляет в хранилище новые значения статистики по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="values">новые значения</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    Task<string> Append(string key, string values);
    
    /// <summary>
    /// очищает из хранилища данные статистики по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    Task<string> Clear(string key);
    
    /// <summary>
    /// отдает подсчитанную статистику по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    Task<string> Stat(string key);
}