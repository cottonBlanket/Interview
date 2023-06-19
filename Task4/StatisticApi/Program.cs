using StatisticApi.Controllers.mapping;
using Task4.Statistics;
using Task4.Statistics.Api;
using Task4.Statistics.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//путь до файла со статистикой
var data = builder.Configuration.GetValue<string>("PathToData");

//добавление зависимостей
builder.Services.AddAutoMapper(typeof(StatisticProfile));
builder.Services.AddTransient<IConsoleStatisticManager, ConsoleStatisticManager>();
builder.Services.AddTransient<IStatisticManager, StatisticManager>();
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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//инициализация потоков для приложения обработки запросов и конмольного приложения
var api = new Thread(app.Run);
var console = new Thread(Task4.Program.Main);

//запуск приложения
api.Start();
console.Start();