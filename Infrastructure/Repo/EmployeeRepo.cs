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
    internal class EmployeeRepo : IEmployee
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepo(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<EmployeeResponse> CreateEmployeesAsync(RegisterUserDTO employee)
        {
            var getUsers = await FindUserByEmailAsync(employee.Email!);
            if (getUsers != null) return new EmployeeResponse(false, "User alredy exist");

            _appDbContext.Add(new ApplicationUser()
            {
                Name = employee.Name,
                Email = employee.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(employee.Password),
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                IdNumber = employee.IdNumber,
                Role = Domine.Role.Employee,
            });

            await _appDbContext.SaveChangesAsync();
            return new EmployeeResponse(true, "Registration are completed");
        }

        public async Task<EmployeeResponse> DeleteEmployeesAsync(string email)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return new EmployeeResponse(false, "Employee not found.");
                }

                _appDbContext.Users.Remove(user);

                await _appDbContext.SaveChangesAsync();

                return new EmployeeResponse(true, "Employee successfully deleted.");
            }
            catch (Exception ex)
            {
                return new EmployeeResponse(false, $"An error occurred while deleting the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> GetEmployeesAsync()
        {
            try
            {
                var users = await _appDbContext.Users
                                                .Where(u => u.Role == Domine.Role.Employee)
                                                .ToListAsync();

                var employeesDtoList = users.Select(u => new EmployeeDto
                {
                    EmployeeId = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    IdNumber = u.IdNumber,
                    Role = u.Role
                });

                return new EmployeeResponse(true, "Clients list retrieved successfully", employeesDtoList);
            }
            catch (Exception ex)
            {
                return new EmployeeResponse(false, ex.Message);
            }
        }

        public async Task<EmployeeResponse> UpdateEmployeesAsync(EmployeeDto employee)
        {
            try
            {
                var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == employee.Email);

                if (existingUser == null)
                {
                    return new EmployeeResponse(false, "Employee not found.");
                }

                existingUser.Name = employee.Name;
                existingUser.Email = employee.Email;
                existingUser.PhoneNumber = employee.PhoneNumber;
                existingUser.Address = employee.Address;
                existingUser.Role = employee.Role;

                await _appDbContext.SaveChangesAsync();

                return new EmployeeResponse(true, "Employee successfully updated.");
            }
            catch (Exception ex)
            {
                return new EmployeeResponse(false, $"An error occurred while updating the employee: {ex.Message}");
            }
        }

        private async Task<ApplicationUser> FindUserByEmailAsync(string Email) =>
            await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
    }
}
