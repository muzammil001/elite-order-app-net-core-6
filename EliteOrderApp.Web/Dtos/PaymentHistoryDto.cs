using EliteOrderApp.Domain.Entities;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace EliteOrderApp.Web.Dtos
{
	public class PaymentHistoryDto
	{
        public int Id { get; set; }
        public DateTime PaidDate { get; set; } = DateTime.Today;

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int PaidAmount { get; set; } = 0;
        public int Balance { get; set; }

        public string Description { get; set; }

        public int OrderId { get; set; }
    }
}
