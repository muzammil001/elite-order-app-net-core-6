using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Web.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;

namespace EliteOrderApp.Web.Models
{
    public class OrderModel
    {
        public OrderDto Order { get; set; }

        public List<Item> Items { get; set; } //Dropdown list
        public List<CustomerDropDownListModel> Customers { get; set; } //Dropdown list
    }
}
