using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteOrderApp.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public int Qty { get; set; } = 1;

    }
}
