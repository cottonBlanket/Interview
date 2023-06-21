namespace Dal.Base.Entities;

/// <summary>
/// базовая сущность для базы данных
/// </summary>
/// <typeparam name="T">тип уникального ключа</typeparam>
public abstract class BaseDal<T> 
{
    /// <summary>
    /// уникальный ключ
    /// </summary>
    public T Id { get; set; }
}