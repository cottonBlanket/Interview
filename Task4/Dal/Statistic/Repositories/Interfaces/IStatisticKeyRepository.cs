using Dal.Base.Interfaces;
using Dal.Statistic.Entities;

namespace Dal.Statistic.Repositories.Interfaces;

public interface IStatisticKeyRepository: IBaseRepository<StatisticKeyDal, int>
{
    public Task<StatisticKeyDal?> GetByName(string name);
}