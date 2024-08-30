using Application.DTOs;
using Domine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IDishes
    {
        Task<DishesResponse> CreateAsync(DishesDto dish);
        Task<DishesResponse> GetAsync();
        Task<DishesResponse> DeleteAsync(int Id);
        Task<DishesResponse> UpdateAsync(DishesDto dish);
    }
}
