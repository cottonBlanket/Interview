using Dal.Base.Entities;
using Dal.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Base;

/// <summary>
/// реализация базового репозитория для работы с бд
/// </summary>
/// <typeparam name="T">тип сущности из бд</typeparam>
/// <typeparam name="TI">тип уникального ключа</typeparam>
public abstract class BaseRepository<T, TI> : IBaseRepository<T, TI> where T : BaseDal<TI>
{
    /// <summary>
    /// соединение с базой данных
    /// </summary>
    protected readonly DataContext _context;
    
    /// <summary>
    /// соединение с таблицой сущности, с которой работает репозиторий
    /// </summary>
    protected readonly DbSet<T> _dbSet;

    /// <summary>
    /// конструктор инициализирующий поле соединения с базой данных
    /// </summary>
    /// <param name="context">соединения с базой данных</param>
    public BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// Вставляет запись в базу данных
    /// </summary>
    /// <param name="dal">Сущность, которую нужно вставить</param>
    /// <returns>Id новой записи</returns>
    public virtual async Task<TI> InsertAsync(T dal)
    {
        var entity = await _dbSet.AddAsync(dal);
        await _context.SaveChangesAsync();
        return entity.Entity.Id;
    }

    /// <summary>
    /// Удаляет запись из бд по ее Id
    /// </summary>
    /// <param name="id">уникальный идентификатор записи</param>
    public virtual async Task DeleteAsync(TI id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
    }

    /// <summary>
    /// Получает данные из таблицы по Id 
    /// </summary>
    /// <param name="id">уникальный идентификатор записи</param>
    /// <returns></returns>
    public virtual async Task<T?> GetAsync(TI id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    /// <summary>
    /// Обновляет данные о записи по Id
    /// </summary>
    /// <param name="dal">Сущность для обновления</param>
    /// <returns>Id записи</returns>
    public virtual async Task<TI> UpdateAsync(T dal)
    {
        _context.Entry(dal).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return dal.Id;
    }
}