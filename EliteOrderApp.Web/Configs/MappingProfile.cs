using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Web.Dtos;

namespace EliteOrderApp.Web.Configs
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
