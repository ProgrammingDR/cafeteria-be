using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IClient
    {
        Task<ClientResponse> GetClientsAsync();
        Task<ClientResponse> DeleteClientAsync(string email);
        Task<ClientResponse> UpdateClientAsync(ClientsDto client);
    }
}
