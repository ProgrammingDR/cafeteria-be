using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IAdmin
    {
        Task<AdminResponse> CreateAdminAsync(RegisterUserDTO admin);
        Task<AdminResponse> GetAdminsAsync();
        Task<AdminResponse> DeleteAdminsAsync(string email);
        Task<AdminResponse> UpdateAdminAsync(AdminDto admin);
    }
}
