using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domine.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public string? clientId { get; set; }
        public int dishId { get; set; }
        public int categoryId { get; set; }
        public int cuantity {  get; set; }
        public OrderState state { get; set; }
        public DateTime orderDate { get; set; }
    }
}
