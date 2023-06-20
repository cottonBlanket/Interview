using System.Text;
using Dal.Statistic.Entities;
using Dal.Statistic.Repositories.Interfaces;
using Task4.Entities;

namespace Task4.Statistics.Api;

/// <summary>
/// сервис для обработки статистики, полученной с сайта системы статистики
/// </summary>
public class StatisticManager: IStatisticManager
{
    /// <summary>
    /// поле для работы с хранилищем статистики
    /// </summary>
    private readonly IStatisticKeyRepository _statisticKeyRepository;
    private readonly IStatisticValueRepository _statisticValueRepository;
    
    /// <summary>
    /// конструктор инициализирующий все поля класс
    /// </summary>
    /// <param name="statisticData">поле для работы с хранилищем статистики</param>
    public StatisticManager(IStatisticKeyRepository statisticKeyRepository, 
        IStatisticValueRepository statisticValueRepository)
    {
        _statisticKeyRepository = statisticKeyRepository;
        _statisticValueRepository = statisticValueRepository;
    }
    
    /// <summary>
    /// добавляет в хранилище статистики новые значения
    /// </summary>
    /// <param name="statistic">новая статистика</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Append(Statistic statistic)
    {
        var response = new StringBuilder();
        
        var strValues = statistic.Values.Split(',').Where(x => x != "").ToList();
        var values = TrySelectToInt(strValues);
        if (values.Count == strValues.Count)
            return "Введены не корректные данные!";
        
        var key = await GetOrCreateKeyAsync(statistic, response);

        if (values.Count > 0)
            await AppendNewValuesAsync(values, key, response);
        
        return response.ToString();
    }

    /// <summary>
    /// удаляет из хранилища статистику по входному ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Clear(string key)
    {
        var statisticKey = await _statisticKeyRepository.GetByName(key);
        if(statisticKey == null)
            return $"Ключ \"{key}\" не найден!";
        _statisticValueRepository.DeleteAllByKey(statisticKey);
        return $"Данные по ключу \"{key}\" успешно удалены!";
    }

    /// <summary>
    /// получает из хранилища подсчитанные статистические данные по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Calculate(string key)
    {
        var statisticKey = await _statisticKeyRepository.GetByName(key);
        if(statisticKey == null)
            return $"Ключ \"{key}\" не найден!";
        var values = _statisticValueRepository.GetAllByKey(statisticKey);
        return values.Average(x => x.StatisticValue).ToString();
    }
    
    private static List<int> TrySelectToInt(IEnumerable<string> values)
    {
        var result = new List<int>();
        foreach (var value in values)
        {
            if (int.TryParse(value, out var a))
                result.Add(a);
        }

        return result;
    }
    
    private async Task AppendNewValuesAsync(List<int> values, StatisticKeyDal key, StringBuilder response)
    {
        foreach (var value in values)
        {
            var statValue = new StatisticValueDal()
            {
                StatisticKey = key,
                StatisticValue = value
            };
            await _statisticValueRepository.InsertAsync(statValue);
        }

        response.Append($"\nНовые значения по ключу \"{key}\" успешно добавлены!");
    }

    private async Task<StatisticKeyDal> GetOrCreateKeyAsync(Statistic statistic, StringBuilder response)
    {
        var key = await _statisticKeyRepository.GetByName(statistic.Key);
        if (key == null)
        {
            key = new StatisticKeyDal() { StatisticKey = statistic.Key };
            var keyId = await _statisticKeyRepository
                .InsertAsync(key);
            key.Id = keyId;
            response.Append($"Ключ \"{key}\" успешно добавлен!");
        }
        return key;
    }
}