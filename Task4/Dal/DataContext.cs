using Dal.Statistic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class DataContext: DbContext
{
    public DbSet<StatisticKeyDal> StatisticKeys { get; set; }
    public DbSet<StatisticValueDal> StatisticValues { get; set; }

    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        Database.EnsureDeleted();   // удаляем бд со старой схемой
        Database.EnsureCreated();   // создаем бд с новой схемой        
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<StatisticKeyDal>().HasIndex(x => x.StatisticKey).IsUnique();
    }
}