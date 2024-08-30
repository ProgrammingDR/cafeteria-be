using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dishes : ControllerBase
    {
        private readonly IDishes _dish;

        public Dishes(IDishes dish)
        {
            _dish = dish;
        }

        [HttpGet("get-dishes")]
        public async Task<ActionResult<DishesResponse>> Get() 
        {
            try
            {
                var result = await _dish.GetAsync();
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-dish")]
        public async Task<ActionResult<DishesResponse>> create(DishesDto dish)
        {
            try
            {
                var result = await _dish.CreateAsync(dish);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-dish")]
        public async Task<ActionResult<DishesResponse>> update(DishesDto dish)
        {
            try
            {
                var result = _dish.UpdateAsync(dish);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-dish")]
        public async Task<ActionResult<DishesResponse>> delete(int id)
        {
            try
            {
                var result = await _dish.DeleteAsync(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
