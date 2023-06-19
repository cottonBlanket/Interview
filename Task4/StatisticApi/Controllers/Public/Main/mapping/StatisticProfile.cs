using AutoMapper;
using StatisticApi.Controllers.dto.request;
using Task4.Entities;

namespace StatisticApi.Controllers.mapping;

/// <summary>
/// класс, отвечающий за преобразование моделей данных для запросов и ответов по обработке статистики
/// </summary>
public class StatisticProfile: Profile
{
    /// <summary>
    /// Констуктор и маппинг
    /// </summary>
    public StatisticProfile()
    {
        CreateMap<KeyValuePairRequest, StatisticDal>()
            .ForMember(dst => dst.Key, opt => opt.MapFrom(src => src.Key))
            .ForMember(dst => dst.Values, opt => opt.MapFrom(src => src.Values));
    }
}