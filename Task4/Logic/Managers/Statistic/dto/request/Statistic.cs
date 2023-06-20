namespace Task4.Entities;

/// <summary>
/// модель данных статистики
/// </summary>
public class Statistic
{
    /// <summary>
    /// ключ статистики
    /// </summary>
    public string Key { get; set; }
    
    /// <summary>
    /// значения статистики
    /// </summary>
    public string? Values { get; set; }
}