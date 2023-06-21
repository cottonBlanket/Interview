using Dal.Base.Interfaces;
using Dal.Statistic.Entities;

namespace Dal.Statistic.Repositories.Interfaces;

/// <summary>
/// репозиторий для работы с сущностью значения статистики
/// </summary>
public interface IStatisticValueRepository: IBaseRepository<StatisticValueDal, int>
{
    /// <summary>
    /// удаляет из бд все значения статистики по входному ключу
    /// </summary>
    /// <param name="key">ключ статистики</param>
    public void DeleteAllByKey(StatisticKeyDal key);
    
    /// <summary>
    /// возвращает список всех значений статистики по входному ключу
    /// </summary>
    /// <param name="key">ключ статистики</param>
    /// <returns>коллекция значений статистики</returns>
    public IEnumerable<StatisticValueDal> GetAllByKey(StatisticKeyDal key);
}