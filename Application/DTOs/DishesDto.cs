using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DishesDto
    {
        public int Id { get; set; }
        public string? dishesName { get; set; }
        public string? dishesDescription { get; set; }
        public string categoryName { get; set; }
        public float dishesPrice { get; set; }
    }
}
