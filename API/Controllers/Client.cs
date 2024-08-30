using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Client : ControllerBase
    {
        private readonly IClient _client;

        public Client(IClient client)
        {
            _client = client;
        }
        
        [HttpGet("get-Clients")]
        public async Task<ActionResult<ClientResponse>> GetClients()
        {
            try
            {
                var result = await _client.GetClientsAsync();
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete-Client")]
        public async Task<ActionResult<ClientResponse>> DeleteClients(string email)
        {
            try
            {
                var result = await _client.DeleteClientAsync(email);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update-Client")]
        public async Task<ActionResult<ClientResponse>> UpdateClients(ClientsDto clients)
        {
            try
            {
                var result = await _client.UpdateClientAsync(clients);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
     }
}
