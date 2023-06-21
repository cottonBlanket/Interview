using Dal.Base.Entities;

namespace Dal.Base.Interfaces;

/// <summary>
/// базовый репозиторий для круд операций
/// </summary>
/// <typeparam name="T">тип сущности, с которой работает репозиторий</typeparam>
/// <typeparam name="TI">тип уникального ключа сущности</typeparam>
public interface IBaseRepository<T, TI> where T : BaseDal<TI>
{
    /// <summary>
    /// Вставляет запись в базу данных
    /// </summary>
    /// <param name="dal">Сущность, которую нужно вставить</param>
    /// <returns>Id новой записи</returns>
    public Task<TI> InsertAsync(T dal);
    
    /// <summary>
    /// Удаляет запись из бд по ее Id
    /// </summary>
    /// <param name="id">уникальный идентификатор записи</param>
    public Task DeleteAsync(TI id);
    
    /// <summary>
    /// Получает данные из таблицы по Id 
    /// </summary>
    /// <param name="id">уникальный идентификатор записи</param>
    /// <returns></returns>
    public Task<T?> GetAsync(TI id);
    
    /// <summary>
    /// Обновляет данные о записи по Id
    /// </summary>
    /// <param name="dal">Сущность для обновления</param>
    /// <returns>Id записи</returns>
    public Task<TI> UpdateAsync(T dal);
}