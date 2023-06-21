namespace Task4.Entities;

/// <summary>
/// модель данных для изменения по ключу
/// </summary>
public class Statistic
{
    /// <summary>
    /// ключ статистики
    /// </summary>
    public string Key { get; set; }
    
    /// <summary>
    /// значения для ключа
    /// </summary>
    public string? Values { get; set; }
    
    /// <summary>
    /// конструктор, инициализирующий все поля класса
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public Statistic(string key, string? values)
    {
        Key = key;
        Values = values;
    }
}