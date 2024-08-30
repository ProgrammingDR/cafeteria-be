using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domine.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public string? employeeId { get; set; }
        public string? clientName { get; set; }
        public DateTime saleDate { get; set; }
    }
}
