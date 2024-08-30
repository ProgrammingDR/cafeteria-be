using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateSaleDto
    {
        public int salesId {  get; set; }
        public int orderId {  get; set; }
        public string? employeeId { get; set; }
        public DateTime saleDate { get; set; }
    }
}
