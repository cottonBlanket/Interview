using Dal.Base.Interfaces;
using Dal.Statistic.Entities;

namespace Dal.Statistic.Repositories.Interfaces;

public interface IStatisticValueRepository: IBaseRepository<StatisticValueDal, int>
{
    public void DeleteAllByKey(StatisticKeyDal key);
    public List<StatisticValueDal> GetAllByKey(StatisticKeyDal key);
}