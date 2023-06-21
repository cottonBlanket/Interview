using Dal.Base.Entities;

namespace Dal.Statistic.Entities;

/// <summary>
/// сущность значения статистики
/// </summary>
public class StatisticValueDal: BaseDal<int>
{
    /// <summary>
    /// значение
    /// </summary>
    public int StatisticValue { get; set; }
    /// <summary>
    /// ключ, к которому относится значение
    /// </summary>
    public StatisticKeyDal StatisticKey { get; set; }
}