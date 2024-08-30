using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrdersResponse(bool flag, string message, IEnumerable<OrdersDto> data = null!);
}
