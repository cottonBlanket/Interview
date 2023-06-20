using Dal.Base;
using Dal.Statistic.Entities;
using Dal.Statistic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Statistic.Repositories;

public class StatisticKeyRepository: BaseRepository<StatisticKeyDal, int>, IStatisticKeyRepository
{
    public StatisticKeyRepository(DataContext context): base(context)
    {
        
    }

    public async Task<StatisticKeyDal?> GetByName(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.StatisticKey == name);
    }
}