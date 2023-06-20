using Dal.Base.Entities;

namespace Dal.Statistic.Entities;

public class StatisticKeyDal: BaseDal<int>
{
    public string StatisticKey { get; set; }
}