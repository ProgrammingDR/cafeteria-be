using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SaleDto
    {
        public int salesId {  get; set; }
        public string? dishName { get; set; }
        public string? categoryName { get; set; }
        public string? clientName { get; set; }
        public string? employeeName { get; set; }
        public int cuantity { get; set; }
        public float price { get; set; }
        public DateTime saleDate { get; set; }
    }
}
