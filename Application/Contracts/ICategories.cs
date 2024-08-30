using Application.DTOs;
using Domine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICategories
    {
        Task<CategoryResponse> CreateAsync(CategoryDto category);
        Task<CategoryResponse> GetAsync();
        Task<CategoryResponse> DeleteAsync(string name);
        Task<CategoryResponse> UpdateAsync(CategoryDto category);
    }
}
