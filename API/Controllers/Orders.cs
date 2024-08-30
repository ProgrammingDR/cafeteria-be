using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Orders : ControllerBase
    {
        private readonly IOrders _orders;

        public Orders(IOrders orders)
        {
            _orders = orders;
        }

        [HttpGet("get-orders")]
        public async Task<ActionResult<OrdersResponse>> get()
        {
            try
            {
                var result = await _orders.GetAsync();
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<OrdersResponse>> create(OrdersDto order)
        {
            try
            {
                var result = await _orders.CreateAsync(order);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-order")]
        public async Task<ActionResult<OrdersResponse>> update(OrdersDto order)
        {
            try
            {
                var result = await _orders.UpdateAsync(order);
                return Ok(result);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-order")]
        public async Task<ActionResult<OrdersResponse>> delete(int id)
        {
            try
            {
                var result = await _orders.DeleteAsync(id);
                return Ok(result);
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
