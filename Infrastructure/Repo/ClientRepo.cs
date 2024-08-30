using Application.Contracts;
using Application.DTOs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    internal class ClientRepo : IClient
    {
        private readonly AppDbContext _appDbContext;
        public ClientRepo(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<ClientResponse> GetClientsAsync()
        {
            try
            {
                var users = await _appDbContext.Users
                                                .Where(u => u.Role == 0)
                                                .ToListAsync();

                var clientsDtoList = users.Select(u => new ClientsDto
                {
                    ClientId = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    IdNumber = u.IdNumber,
                    Role = u.Role
                });

                return new ClientResponse(true, "Clients list retrieved successfully", clientsDtoList);
            }
            catch (Exception ex)
            {
                return new ClientResponse(false, ex.Message);
            }
        }

        public async Task<ClientResponse> DeleteClientAsync(string email)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return new ClientResponse(false, "Client not found.");
                }

                _appDbContext.Users.Remove(user);

                await _appDbContext.SaveChangesAsync();

                return new ClientResponse(true, "Client successfully deleted.");
            }
            catch (Exception ex)
            {
                return new ClientResponse(false, $"An error occurred while deleting the client: {ex.Message}");
            }
        }

        public async Task<ClientResponse> UpdateClientAsync(ClientsDto client)
        {
            try
            {
                var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == client.Email);

                if (existingUser == null)
                {
                    return new ClientResponse(false, "Client not found.");
                }

                existingUser.Name = client.Name;
                existingUser.Email = client.Email;
                existingUser.PhoneNumber = client.PhoneNumber;
                existingUser.Address = client.Address;
                existingUser.Role = client.Role;

                await _appDbContext.SaveChangesAsync();

                return new ClientResponse(true, "Client successfully updated.");
            }
            catch (Exception ex)
            {
                return new ClientResponse(false, $"An error occurred while updating the client: {ex.Message}");
            }
        }

    }
}
