using Dal;
using Dal.Statistic.Repositories;
using Dal.Statistic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StatisticApi.Controllers.mapping;
using Task4.Statistics;
using Task4.Statistics.Api;
using Task4.Statistics.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// подключение к бд
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Singleton);

//добавление зависимостей
builder.Services.AddAutoMapper(typeof(StatisticProfile));
builder.Services.AddTransient<IConsoleStatisticManager, ConsoleStatisticManager>();
builder.Services.AddTransient<IStatisticManager, StatisticManager>();
builder.Services.AddTransient<IStatisticKeyRepository, StatisticKeyRepository>();
builder.Services.AddTransient<IStatisticValueRepository, StatisticValueRepository>();
builder.Services.AddSingleton<Task4.Program>();


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

// проверка на необходимость обновления базы данных
//указывается в appsettings.json
//ПОСЛЕ ПЕРВОГО ЗАПУСКА ПРИЛОЖЕНИЯ РЕКОМЕНДУЕТСЯ УКАЗАТЬ false 
//ИНАЧЕ ПРИ ПЕРЕЗАПУСКЕ ВСЕ ДАННЫЕ УДАЛЯТСЯ
if (bool.Parse(builder.Configuration.GetSection("UpdateDatabase")?.Value))
{
    var context = app.Services.GetService<DataContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Statistic}/{action=Home}");

//инициализация потоков для приложения обработки запросов и конмольного приложения
var api = new Thread(app.Run);
var service = app.Services.GetService<Task4.Program>();
var console = new Thread(service.ReadConsoleCommand);
//запуск приложения
api.Start();
console.Start();