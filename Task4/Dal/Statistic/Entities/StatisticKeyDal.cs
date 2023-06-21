using Dal.Base.Entities;
using Task4.Statistics.Api.enums;

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
    
    /// <summary>
    /// метод подсчета значения по ключу
    /// </summary>
    public  CountingMethod CountingMethod { get; set; }
}