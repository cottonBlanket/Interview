using Dal.Base;
using Dal.Statistic.Entities;
using Dal.Statistic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Statistic.Repositories;

public class StatisticValueRepository: BaseRepository<StatisticValueDal, int>, IStatisticValueRepository
{
    public StatisticValueRepository(DataContext context): base(context)
    {
        
    }

    public void DeleteAllByKey(StatisticKeyDal key)
    {
        var valuesByKey = GetAllByKey(key);
        _dbSet.RemoveRange(valuesByKey);
    }

    public List<StatisticValueDal> GetAllByKey(StatisticKeyDal key)
    {
        return _dbSet
            .Include(x => x.StatisticKey)
            .Where(x => x.StatisticKey == key)
            .ToList();
    }
}