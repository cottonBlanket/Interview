using Dal.Base;
using Dal.Statistic.Entities;
using Dal.Statistic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Statistic.Repositories;

/// <summary>
/// реализация репозитория для работы с сущностью значения статистики
/// </summary>
public class StatisticValueRepository: BaseRepository<StatisticValueDal, int>, IStatisticValueRepository
{
    /// <summary>
    /// конструктор, инициализирующий поле для соединения с бд
    /// </summary>
    /// <param name="context">соединения с бд</param>
    public StatisticValueRepository(DataContext context): base(context)
    {
        
    }

    /// <summary>
    /// удаляет из бд все значения статистики по входному ключу
    /// </summary>
    /// <param name="key">ключ статистики</param>
    public void DeleteAllByKey(StatisticKeyDal key)
    {
        var valuesByKey = GetAllByKey(key);
        _dbSet.RemoveRange(valuesByKey);
    }

    /// <summary>
    /// возвращает коллекцию из значений статистики по входному ключу
    /// </summary>
    /// <param name="key">ключ статистики</param>
    /// <returns>коллекция значений статистики</returns>
    public IEnumerable<StatisticValueDal> GetAllByKey(StatisticKeyDal key)
    {
        return _dbSet
            .Include(x => x.StatisticKey)
            .Where(x => x.StatisticKey == key)
            .ToList();
    }
}