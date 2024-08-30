using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories : ControllerBase
    {
        private readonly ICategories _category;

        public Categories(ICategories category)
        {
            _category = category;
        }

        [HttpGet("get-categories")]
        public async Task<ActionResult<CategoryResponse>> Get() 
        {
            try
            {
                var result = await _category.GetAsync();
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-category")]
        public async Task<ActionResult<CategoryResponse>> create(CategoryDto category)
        {
            try
            {
                var result = await _category.CreateAsync(category);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<ActionResult<CategoryResponse>> update(CategoryDto category)
        {
            try
            {
                var result = await _category.UpdateAsync(category);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category")]
        public async Task<ActionResult<CategoryResponse>> delete(string categoryName)
        {
            try
            {
                var result = await _category.DeleteAsync(categoryName);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
