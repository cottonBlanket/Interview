using Dal.Statistic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal;

/// <summary>
/// класс, отвечащий за соединение с базой данных
/// </summary>
public class DataContext: DbContext
{
    /// <summary>
    /// таблица сущностей значений статистики
    /// </summary>
    public DbSet<StatisticKeyDal> StatisticKeys { get; set; }
    /// <summary>
    /// таблица сущностей значений статистики
    /// </summary>
    public DbSet<StatisticValueDal> StatisticValues { get; set; }

    /// <summary>
    /// конструктор, инициализирующий настройки соединения с базой данных
    /// </summary>
    /// <param name="options"></param>
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {      
    }
    
    /// <summary>
    /// асинхронно сохраняет изменения в бд
    /// </summary>
    /// <returns>асинхронная задача</returns>
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    /// <summary>
    /// настраивает парметры создания сущностей в базе данных
    /// </summary>
    /// <param name="builder">создатель сущностей</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<StatisticKeyDal>().HasIndex(x => x.StatisticKey).IsUnique();
    }
}