using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record ClientResponse(bool Flag, string Message = null!, IEnumerable<ClientsDto> data = null!);
}
