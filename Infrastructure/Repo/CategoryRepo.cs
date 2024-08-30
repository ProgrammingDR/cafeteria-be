using Application.Contracts;
using Application.DTOs;
using Domine.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    internal class CategoryRepo : ICategories
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CategoryResponse> CreateAsync(CategoryDto category)
        {
            var categoryExist = await FindCategoryByNameAsync(category.CategoryName!);
            if (categoryExist != null) return new CategoryResponse(false, "La categoria ya existe");
            
            _appDbContext.Add(new Categories 
            { 
                CategoryName = category.CategoryName 
            });

            await _appDbContext.SaveChangesAsync();
            return new CategoryResponse(true, "Categoria agregada");
        }

        public async Task<CategoryResponse> DeleteAsync(string name)
        {
            try
            {
                var categoryExist = await FindCategoryByNameAsync(name!);

                if (categoryExist == null) return new CategoryResponse(false, "Client not found.");
                

                _appDbContext.Category.Remove(categoryExist);

                await _appDbContext.SaveChangesAsync();

                return new CategoryResponse(true, "Client successfully deleted.");
            }
            catch (Exception ex)
            {
                return new CategoryResponse(false, $"An error occurred while deleting the client: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> GetAsync()
        {
            try
            {
                var category = await _appDbContext.Category.ToListAsync();

                var categoryListDto = category.Select(u => new CategoryDto
                {
                    CategoryName = u.CategoryName,
                });

                return new CategoryResponse(true, "Clients list retrieved successfully", categoryListDto);
            }
            catch (Exception ex)
            {
                return new CategoryResponse(false, ex.Message);
            }
        }

        public async Task<CategoryResponse> UpdateAsync(CategoryDto category)
        {
            try
            {
                var categoryExist = await FindCategoryByNameAsync(category.CategoryName!);

                if (categoryExist == null) return new CategoryResponse(false, "Client not found.");

                categoryExist.CategoryName = category.CategoryName;

                await _appDbContext.SaveChangesAsync();

                return new CategoryResponse(true, "Client successfully updated.");
            }
            catch (Exception ex)
            {
                return new CategoryResponse(false, $"An error occurred while updating the client: {ex.Message}");
            }
        }

        private async Task<Categories> FindCategoryByNameAsync(string name) =>
            await _appDbContext.Category.FirstOrDefaultAsync(u => u.CategoryName == name);
    }
}
