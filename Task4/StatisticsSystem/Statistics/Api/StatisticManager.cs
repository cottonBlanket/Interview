using Task4.Entities;
using Task4.Statistics.Interfaces;

namespace Task4.Statistics.Api;

/// <summary>
/// сервис для обработки статистики, полученной с сайта системы статистики
/// </summary>
public class StatisticManager: IStatisticManager
{
    /// <summary>
    /// поле для работы с хранилищем статистики
    /// </summary>
    private readonly IStatisticData _statisticData;
    
    /// <summary>
    /// конструктор инициализирующий все поля класс
    /// </summary>
    /// <param name="statisticData">поле для работы с хранилищем статистики</param>
    public StatisticManager(IStatisticData statisticData)
    {
        _statisticData = statisticData;
    }
    
    /// <summary>
    /// добавляет в хранилище статистики новые значения
    /// </summary>
    /// <param name="statistic">новая статистика</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Append(StatisticDal statistic)
    {
        var values = statistic.Values?.Replace(',', ' ');
        var response = await _statisticData.Append(statistic.Key, values);
        return response;
    }

    /// <summary>
    /// удаляет из хранилища статистику по входному ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Clear(string key)
    {
        var response = await _statisticData.Clear(key);
        return response;
    }

    /// <summary>
    /// получает из хранилища подсчитанные статистические данные по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Calculate(string key)
    {
        var response = await _statisticData.Stat(key);
        return response;
    }
}