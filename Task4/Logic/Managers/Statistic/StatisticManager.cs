using System.Text;
using Dal.Statistic.Entities;
using Dal.Statistic.Repositories.Interfaces;
using Task4.Entities;

namespace Task4.Statistics.Api;

/// <summary>
/// сервис для обработки статистики
/// </summary>
public class StatisticManager: IStatisticManager
{
    /// <summary>
    /// репозиторий для работы с сущностью ключа статистики
    /// </summary>
    private readonly IStatisticKeyRepository _statisticKeyRepository;
    /// <summary>
    /// репозиторий для работы с сущностью значения статистики
    /// </summary>
    private readonly IStatisticValueRepository _statisticValueRepository;
    
    /// <summary>
    /// конструктор инициализирующий все поля класс
    /// </summary>
    /// <param name="statisticKeyRepository">репозиторий для работы с сущностью ключа статистики</param>
    /// <param name="statisticValueRepository">репозиторий для работы с сущностью значения статистики</param>
    public StatisticManager(IStatisticKeyRepository statisticKeyRepository, 
        IStatisticValueRepository statisticValueRepository)
    {
        _statisticKeyRepository = statisticKeyRepository;
        _statisticValueRepository = statisticValueRepository;
    }
    
    /// <summary>
    /// принимает новую статистику, получает или создает новый ключ статистики,
    /// добавляет новые значения по ключу
    /// возвращает сообщение, информирующее о проделанных действиях
    /// </summary>
    /// <param name="statistic">новая статистика</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Append(Statistic statistic)
    {
        var response = new StringBuilder();
        
        var strValues = statistic.Values.Split(',').Where(x => x != "").ToList();
        var values = TrySelectToInt(strValues);
        if (values.Count != strValues.Count)
            return "Введены не корректные данные!";
        
        var key = await GetOrCreateKeyAsync(statistic, response);

        if (values.Count > 0)
        {
            await AppendNewValuesAsync(values, key);
            response.Append($"\nНовые значения по ключу \"{key.StatisticKey}\" успешно добавлены!");
        }

        return response.ToString();
    }

    /// <summary>
    /// по входному ключу статистики удаляет все значения
    /// если входной ключ правильный - сообщает об успешному удалении
    /// если ключа не найден - сообщает об этом
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
    /// по входному ключу статистики получает все значения по нему
    /// и возвращает среднее по всем значениям
    /// если ключ не найден - сообщает об этом
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
    
    /// <summary>
    /// преобразовывает коллекция строк в коллекцию целых чисел
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// создает новые значения статистики
    /// </summary>
    /// <param name="values">список значений для сущностей статистики</param>
    /// <param name="key">ключ статистики</param>
    private async Task AppendNewValuesAsync(List<int> values, StatisticKeyDal key)
    {
        foreach (var value in values)
        {
            var statValue = new StatisticValueDal
            {
                StatisticKey = key,
                StatisticValue = value
            };
            await _statisticValueRepository.InsertAsync(statValue);
        }
    }

    /// <summary>
    /// проверяет существует ли входной ключ
    /// если ключ найден - возвращает его
    /// при отсутствии - создает, записывает сообщение об этом и возвращает созданнй ключ
    /// </summary>
    /// <param name="statistic">новая статистика</param>
    /// <param name="response">билдер сообщения</param>
    /// <returns>асинхронное действие, возвращающее сущнсоть ключа статистики</returns>
    private async Task<StatisticKeyDal> GetOrCreateKeyAsync(Statistic statistic, StringBuilder response)
    {
        var key = await _statisticKeyRepository.GetByName(statistic.Key);
        if (key == null)
        {
            key = new StatisticKeyDal() { StatisticKey = statistic.Key };
            var keyId = await _statisticKeyRepository
                .InsertAsync(key);
            key.Id = keyId;
            response.Append($"Ключ \"{key.StatisticKey}\" успешно добавлен!");
        }
        return key;
    }
}