using Dal.Base.Entities;

namespace Dal.Statistic.Entities;

/// <summary>
/// сущность ключа статистики
/// </summary>
public class StatisticKeyDal: BaseDal<int>
{
    /// <summary>
    /// значение ключа
    /// </summary>
    public string StatisticKey { get; set; }
}