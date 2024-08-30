using Domine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrdersDto
    {
        public int OrdersId { get; set; }
        public string? clientId { get; set; }
        public string? dishName { get; set; }
        public string? categoryName { get; set; }
        public int cuantity { get; set; }
        public OrderState state { get; set; }
        public DateTime orderDate { get; set; }
    }
}
