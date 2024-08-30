using Application.Contracts;
using Application.DTOs;
using Domine.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    internal class AdminRepo : IAdmin
    {
        private readonly AppDbContext _appDbContext;
        public AdminRepo(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<AdminResponse> CreateAdminAsync(RegisterUserDTO admin)
        {
            var getUsers = await FindUserByEmailAsync(admin.Email!);
            if (getUsers != null) return new AdminResponse(false, "User alredy exist");

            _appDbContext.Add(new ApplicationUser()
            {
                Name = admin.Name,
                Email = admin.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(admin.Password),
                PhoneNumber = admin.PhoneNumber,
                Address = admin.Address,
                IdNumber = admin.IdNumber,
                Role = Domine.Role.Admin,
            });

            await _appDbContext.SaveChangesAsync();
            return new AdminResponse(true, "Registration are completed");
        }

        public async Task<AdminResponse> DeleteAdminsAsync(string email)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return new AdminResponse(false, "Employee not found.");
                }

                _appDbContext.Users.Remove(user);

                await _appDbContext.SaveChangesAsync();

                return new AdminResponse(true, "Employee successfully deleted.");
            }
            catch (Exception ex)
            {
                return new AdminResponse(false, $"An error occurred while deleting the employee: {ex.Message}");
            }
        }

        public async Task<AdminResponse> GetAdminsAsync()
        {
            try
            {
                var users = await _appDbContext.Users
                                                .Where(u => u.Role == Domine.Role.Admin)
                                                .ToListAsync();

                var adminsDtoList = users.Select(u => new AdminDto
                {
                    AdminId = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    IdNumber = u.IdNumber,
                    Role = u.Role
                });

                return new AdminResponse(true, "Clients list retrieved successfully", adminsDtoList);
            }
            catch (Exception ex)
            {
                return new AdminResponse(false, ex.Message);
            }
        }

        public async Task<AdminResponse> UpdateAdminAsync(AdminDto admin)
        {
            try
            {
                var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == admin.Email);

                if (existingUser == null)
                {
                    return new AdminResponse(false, "Employee not found.");
                }

                existingUser.Name = admin.Name;
                existingUser.Email = admin.Email;
                existingUser.PhoneNumber = admin.PhoneNumber;
                existingUser.Address = admin.Address;
                existingUser.Role = admin.Role;

                await _appDbContext.SaveChangesAsync();

                return new AdminResponse(true, "Employee successfully updated.");
            }
            catch (Exception ex)
            {
                return new AdminResponse(false, $"An error occurred while updating the employee: {ex.Message}");
            }
        }

        private async Task<ApplicationUser> FindUserByEmailAsync(string Email) =>
           await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
    }
}
