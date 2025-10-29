using AutoMapper;
using OrderManagement.Core.DTOs;
using OrderManagementSystem;


namespace OrderManagement.Core.Mapping
{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<CreateOrderItemDto, OrderItem>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();

    }
}
}
