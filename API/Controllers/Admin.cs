using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin : ControllerBase
    {
        private readonly IAdmin _admin;

        public Admin(IAdmin admin)
        {
            _admin = admin;
        }

        [HttpPost("create-Admin")]
        public async Task<ActionResult<AdminResponse>> Create(RegisterUserDTO user)
        {
            try
            {
                var result = await _admin.CreateAdminAsync(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-Admin")]
        public async Task<ActionResult<EmployeeResponse>> Get()
        {
            try
            {
                var result = await _admin.GetAdminsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete-Admin")]
        public async Task<ActionResult<AdminResponse>> Delete(string email)
        {
            try
            {
                var result = await _admin.DeleteAdminsAsync(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update-Admin")]
        public async Task<ActionResult<AdminResponse>> UpdateClients(AdminDto admin)
        {
            try
            {
                var result = await _admin.UpdateAdminAsync(admin);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
