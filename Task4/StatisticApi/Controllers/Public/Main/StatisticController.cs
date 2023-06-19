using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StatisticApi.Controllers.dto.request;
using StatisticApi.Controllers.dto.response;
using Task4.Entities;
using Task4.Statistics.Api;

namespace StatisticApi.Controllers;

/// <summary>
/// контроллер, принимающий запросы для обработки статистики
/// </summary>
public class StatisticController : Controller
{
    /// <summary>
    /// обработчик статистики
    /// </summary>
    private readonly IStatisticManager _statisticManager;
    
    /// <summary>
    /// маппер - преобразователь типов
    /// </summary>
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Конструктор инициализирубщий поля класса
    /// </summary>
    /// <param name="statisticManager">поле для обработки статистики</param>
    /// <param name="mapper">поле для преобразования типов</param>
    public StatisticController(IStatisticManager statisticManager, IMapper mapper)
    {
        _statisticManager = statisticManager;
        _mapper = mapper;
    }
    
    /// <summary>
    /// обрабатывает запрос на получение главной страницы системы статистики
    /// </summary>
    /// <returns>главная страница системы статистики</returns>
    [HttpGet]
    public IActionResult Home()
    {
        return View();
    }

    /// <summary>
    /// обрабатывает запрос на получение страницы для добавления новой статистики
    /// </summary>
    /// <returns>страница для добавления новой статистики</returns>
    [HttpGet]
    public IActionResult Append()
    {
        return View(new KeyValuePairRequest());
    }
    
    /// <summary>
    /// обрабатывает запрос на добавление новой статистики
    /// </summary>
    /// <param name="request">тело запроса - новая статистика</param>
    /// <returns>страница с отображением сообщения, содержащее информацию о совершенных действиях со статистикой</returns>
    [HttpPost]
    public async Task<IActionResult> Append(KeyValuePairRequest request)
    {
        var statistic = _mapper.Map<StatisticDal>(request);
        var response = await _statisticManager.Append(statistic);
        return View("Message", new MessageResponse() {Message = response});
    }
    
    /// <summary>
    /// обрабатывает запрос на получение страницы для очистки статистики
    /// </summary>
    /// <returns>страница для очистики статистики</returns>
    [HttpGet]
    public IActionResult Clear()
    {
        return View(new KeyRequest());
    }
    
    /// <summary>
    /// обрабатывает запрос на очистку статистики по ключу
    /// </summary>
    /// <param name="request">тело запроса - ключ статистики</param>
    /// <returns>страница с отображением сообщения, содержащее информацию о совершенных действиях со статистикой</returns>
    [HttpPost]
    public async Task<IActionResult> Clear(KeyRequest request)
    {
        var response = await _statisticManager.Clear(request.Key);
        return View("Message", new MessageResponse() {Message = response});
    }
    
    /// <summary>
    /// обрабатывает запрос на получение страницы для подсчета статистики
    /// </summary>
    /// <returns>страница для подсчета статистики</returns>
    [HttpGet]
    public IActionResult Calculate()
    {
        return View(new KeyRequest());
    }
    
    /// <summary>
    /// обрабатывает запрос на получение статистических данных по ключу
    /// </summary>
    /// <param name="request">тело запроса - ключ сатистики</param>
    /// <returns>страница с отображением сообщения, содержащее информацию о совершенных действиях со статистикой</returns>
    [HttpPost]
    public async Task<IActionResult> Calculate(KeyRequest request)
    {
        var response = await _statisticManager.Calculate(request.Key);
        return View("Message", new MessageResponse() {Message = response});
    }

    /// <summary>
    /// обрабатывает запрос на получение страницы, содержащее входное сообщение
    /// </summary>
    /// <param name="message">тело запроса - модель входного сообщения</param>
    /// <returns>страница содержащая входное сообщение</returns>
    [HttpPost]
    public IActionResult Message(MessageResponse message)
    {
        return View(message);
    }
}