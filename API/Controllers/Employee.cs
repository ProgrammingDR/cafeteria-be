using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Employee : ControllerBase
    {
        private readonly IEmployee _employee;

        public Employee(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpPost("create-Employee")]
        public async Task<ActionResult<EmployeeResponse>> Create(RegisterUserDTO user)
        {
            try
            {
                var result = await _employee.CreateEmployeesAsync(user);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-Employees")]
        public async Task<ActionResult<EmployeeResponse>> Get()
        {
            try
            {
                var result = await _employee.GetEmployeesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete-Employees")]
        public async Task<ActionResult<EmployeeResponse>> Delete(string email)
        {
            try
            {
                var result = await _employee.DeleteEmployeesAsync(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update-Employee")]
        public async Task<ActionResult<EmployeeResponse>> UpdateClients(EmployeeDto employee)
        {
            try
            {
                var result = await _employee.UpdateEmployeesAsync(employee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
