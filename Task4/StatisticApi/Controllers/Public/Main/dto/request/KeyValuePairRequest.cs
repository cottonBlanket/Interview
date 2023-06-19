namespace StatisticApi.Controllers.dto.request;

/// <summary>
/// модель данных для запросов по ключу со значениям
/// </summary>
public record KeyValuePairRequest
{
    /// <summary>
    /// ключ
    /// </summary>
    public string Key { get; init; }
    
    /// <summary>
    /// значения
    /// </summary>
    public string? Values { get; init; }
}