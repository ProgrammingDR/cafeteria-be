using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record DishesResponse(bool flag, string message, IEnumerable<DishesDto> data = null!);
}
