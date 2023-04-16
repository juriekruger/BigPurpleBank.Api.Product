using AutoMapper;
using BigPurpleBank.Api.Product.Common.Extensions;
using BigPurpleBank.Api.Product.Model.Dto;
using BigPurpleBank.Api.Product.Model.Responses.Product;

namespace BigPurpleBank.Api.Product.Model.Mapper;

public class ProductMapperProfile: Profile
{
    public ProductMapperProfile()
    {
        CreateMap<ProductDto, ProductViewModel>()
            .ForMember(x=> x.EffectiveFrom, src=> src.MapFrom(s=> s.EffectiveFromUnix.FromUnixTime()))
            .ForMember(x=> x.EffectiveTo, src=> src.MapFrom(s=> s.EffectiveToUnix.FromUnixTime()))
            .ForMember(x=> x.LastUpdated, src=> src.MapFrom(s=> s.LastUpdatedUnix.FromUnixTime()))
            ;
    }
}