using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.WebApi.Dtos;

namespace EliteOrderApp.WebApi.Configs
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<Customer, CustomerDto>().ReverseMap();
			CreateMap<Item, ItemDto>().ReverseMap();
			CreateMap<Cart, CartDto>().ReverseMap();
		}
	}
}
