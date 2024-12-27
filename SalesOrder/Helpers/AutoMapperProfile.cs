using AutoMapper;
using SalesOrder.DTOs;
using SalesOrder.Entities;

namespace SalesOrder.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<OrderDto, SoOrder>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore()) // Items handled manually
            .ReverseMap()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => (decimal)src.Items.Sum(i=> Math.Round(i.Quantity * i.Price, 2))))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<ItemDto, SoItem>()
            .ForMember(dest => dest.SoOrderId, opt => opt.Ignore()) // Set manually
            .ReverseMap()
            .ForMember(dest => dest.SoItemId, opt => opt.MapFrom(src => src.SoItemId)); // Ensure mapping

        CreateMap<CustomerDto, ComCustomer>().ReverseMap();
    }
}