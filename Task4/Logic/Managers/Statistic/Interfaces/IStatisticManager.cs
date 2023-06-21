using Task4.Entities;

namespace Task4.Statistics.Api;

/// <summary>
/// интерфейс обработчика статистики полученной
/// </summary>
public interface IStatisticManager
{
    /// <summary>
    /// добавляет новую статистику
    /// </summary>
    /// <param name="statistic">статистика</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public Task<string> Append(Statistic statistic);
    
    /// <summary>
    /// удаляет статистику по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public Task<string> Clear(string key);
    
    /// <summary>
    /// считает статистических данные по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public Task<string> Calculate(string key);

    /// <summary>
    /// изменяет метод для подсчета значений статистики
    /// </summary>
    /// <param name="statistic">новые данные</param>
    /// <returns>асинхронное действие, возвращающее сообщение</returns>
    public Task<string> ChangeMethod(Statistic statistic);
}