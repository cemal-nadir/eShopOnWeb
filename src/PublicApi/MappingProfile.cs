using System.Linq;
using AutoMapper;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;

namespace Microsoft.eShopWeb.PublicApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDto>();
        CreateMap<CatalogType, CatalogTypeDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Type));
        CreateMap<CatalogBrand, CatalogBrandDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Brand));

        #region Orders

        CreateMap<Order, OrderDto>()
            .ForMember(d => d.Total, m => m.MapFrom(x => x.OrderItems.Sum(y => y.Units * y.UnitPrice))).ReverseMap();
        CreateMap<Order, OrdersDto>()
            .ForMember(d => d.Total, m => m.MapFrom(x => x.OrderItems.Sum(y => y.Units * y.UnitPrice)));

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<OrderState, OrderStateDto>().ReverseMap();
        CreateMap<CatalogItemOrdered, CatalogItemOrderedDto>().ReverseMap();

        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemsDto>();



        #endregion
    }
}
