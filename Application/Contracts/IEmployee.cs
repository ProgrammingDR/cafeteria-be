using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IEmployee
    {
        Task<EmployeeResponse> CreateEmployeesAsync(RegisterUserDTO employee);
        Task<EmployeeResponse> GetEmployeesAsync();
        Task<EmployeeResponse> DeleteEmployeesAsync(string email);
        Task<EmployeeResponse> UpdateEmployeesAsync(EmployeeDto employee);
    }
}
