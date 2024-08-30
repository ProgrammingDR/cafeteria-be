using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sales : ControllerBase
    {
        private readonly ISales _sales;

        public Sales(ISales sales)
        {
            _sales = sales;
        }

        [HttpGet("get-sales")]
        public async Task<ActionResult<SalesResponse>> get()
        {
            try
            {
                var result = await _sales.GetAsync();
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-sale")]
        public async Task<ActionResult<SalesResponse>> create(CreateSaleDto sale)
        {
            try
            {
                var result = await _sales.CreateAsync(sale);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-sale")]
        public async Task<ActionResult<SalesResponse>> update(CreateSaleDto sale)
        {
            try
            {
                var result = await _sales.UpdateAsync(sale);
                return Ok(result);
            }catch(Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-sale")]
        public async Task<ActionResult<SalesResponse>> delete(int id)
        {
            try
            {
                var result = await _sales.DeleteAsync(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
