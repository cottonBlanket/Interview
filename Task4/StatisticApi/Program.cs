using Dal;
using Dal.Statistic.Repositories;
using Dal.Statistic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StatisticApi.Controllers.mapping;
using Task4.Statistics;
using Task4.Statistics.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//путь до файла со статистикой
var data = builder.Configuration.GetValue<string>("PathToData");

// подключение к бд
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//добавление зависимостей
builder.Services.AddAutoMapper(typeof(StatisticProfile));
builder.Services.AddTransient<IConsoleStatisticManager, ConsoleStatisticManager>();
builder.Services.AddTransient<IStati
builder.Services.AddScoped<IStatisticKeyRepository, StatisticKeyRepository>();
builder.Services.AddScoped<IStatisticValueRepository, StatisticValueRepository>();
builder.Services.AddSingleton<IStatisticData, StatisticsData>();

//добавление контролллеров и маршрутизации
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();

//создание приложения обработки запросов
var app = builder.Build();

//подключение мидлвееров
app.UseHttpsRedirection();
app.UseRouting();

// Откючаем (комментируем) если не требуется отчистка бд 
// т.к. все данные из бд будут удаленны
using (var scope = 
       app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<DataContext>())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//инициализация потоков для приложения обработки запросов и конмольного приложения
var api = new Thread(app.Run);
var console = new Thread(Task4.Program.Main);

//запуск приложения
api.Start();
//console.Start();