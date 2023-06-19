namespace StatisticApi.Controllers.dto.request;

/// <summary>
/// модель входных данных для запросов по ключу
/// </summary>
public record KeyRequest
{
    /// <summary>
    /// ключ 
    /// </summary>
    public string Key { get; init; }
}