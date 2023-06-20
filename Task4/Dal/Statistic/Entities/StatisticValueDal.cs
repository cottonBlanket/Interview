using Dal.Base.Entities;

namespace Dal.Statistic.Entities;

public class StatisticValueDal: BaseDal<int>
{
    public int StatisticValue { get; set; }
    public StatisticKeyDal StatisticKey { get; set; }
}