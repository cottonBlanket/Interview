using System.ComponentModel.DataAnnotations;

namespace Task4.Statistics.Api.enums;

/// <summary>
/// методы подсчета значений статистики
/// </summary>
public enum CountingMethod
{
    /// <summary>
    /// среднее арифметическое
    /// </summary>
    [Display(Name = "Среднее")]
    Avg,
    /// <summary>
    /// сумма
    /// </summary>
    [Display(Name = "Сумма")]
    Sum
}