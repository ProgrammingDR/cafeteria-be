using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record EmployeeResponse(bool Flag, string Message = null!, IEnumerable<EmployeeDto> data = null!);
}
