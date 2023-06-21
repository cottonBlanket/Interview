using Dal.Base;
using Dal.Statistic.Entities;
using Dal.Statistic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Statistic.Repositories;

/// <summary>
/// реализация репозитория для работы с сущностью ключа статистики
/// </summary>
public class StatisticKeyRepository: BaseRepository<StatisticKeyDal, int>, IStatisticKeyRepository
{
    /// <summary>
    /// конструктор инициализирующий поле для соединения с бд
    /// </summary>
    /// <param name="context">соединение с бд</param>
    public StatisticKeyRepository(DataContext context): base(context)
    {
        
    }

    /// <summary>
    /// возвращает сущность ключа статистики по его значению
    /// </summary>
    /// <param name="name">значение ключа</param>
    /// <returns>ключ статистики</returns>
    public async Task<StatisticKeyDal?> GetByName(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.StatisticKey == name);
    }
}