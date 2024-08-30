using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record SalesResponse(bool flag, string message, IEnumerable<SaleDto> data = null!);
}
