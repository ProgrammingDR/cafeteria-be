using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domine.Entities
{
    public class Dishes
    {
        [Key]
        public int dishesId { get; set; }
        public string? dishesName { get; set;}
        public string? dishesDescription { get; set;}
        public int categoryId { get; set; }
        public float dishesPrice {  get; set; }
    }
}
