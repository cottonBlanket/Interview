using Dal.Base.Interfaces;
using Dal.Statistic.Entities;

namespace Dal.Statistic.Repositories.Interfaces;

/// <summary>
/// репозиторий для работы с сущнстью ключа статистики
/// </summary>
public interface IStatisticKeyRepository: IBaseRepository<StatisticKeyDal, int>
{
    /// <summary>
    /// возвращает ключ статистики по его значению
    /// </summary>
    /// <param name="name">значение ключа</param>
    /// <returns>ключ</returns>
    public Task<StatisticKeyDal?> GetByName(string name);
}