using Application.Contracts;
using Application.DTOs;
using Domine.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    internal class DishRepo: IDishes
    {
        private readonly AppDbContext _appDbContext;

        public DishRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DishesResponse> CreateAsync(DishesDto dish)
        {
            try
            {
                var existingDish = await FindDishByIdAsync(dish.Id);
                if (existingDish != null) return new DishesResponse(false, "Dish alredy exist");

                var categoryId = await _appDbContext.Category.Where(c => c.CategoryName == dish.categoryName).Select(i => i.CategoryId).FirstOrDefaultAsync();

                var Dish = new Dishes
                {
                    dishesName = dish.dishesName,
                    dishesDescription = dish.dishesDescription,
                    categoryId = categoryId,
                    dishesPrice = dish.dishesPrice,
                };

                _appDbContext.Add(Dish);
                await _appDbContext.SaveChangesAsync();

                return new DishesResponse(true, "Dish added successfully");
            }catch (Exception ex)
            {
                return new DishesResponse(false, ex.Message);
            }
        }

        public async Task<DishesResponse> DeleteAsync(int Id)
        {
            try
            {
                var existingDish = await FindDishByIdAsync(Id);

                if (existingDish == null) return new DishesResponse(false, "Order not found");

                _appDbContext.Remove(existingDish);
                await _appDbContext.SaveChangesAsync();

                return new DishesResponse(true, "Order deleted successfully");
            }
            catch (Exception ex)
            {
                return new DishesResponse(false, ex.Message);
            }
        }

        public async Task<DishesResponse> GetAsync()
        {
            try
            {
                var DishData = await(from dish in _appDbContext.Dish
                                       join category in _appDbContext.Category
                                       on dish.categoryId equals category.CategoryId
                                       select new DishesDto
                                       {
                                           Id = dish.dishesId,
                                           dishesName = dish.dishesName!,
                                           dishesDescription = dish.dishesDescription,
                                           categoryName = category.CategoryName!,
                                           dishesPrice = dish.dishesPrice
                                       }).ToListAsync();

                return new DishesResponse(true, "Dishes listed successfully", DishData);
            }
            catch (Exception ex)
            {
                return new DishesResponse(false, ex.Message);
            }
        }

        public async Task<DishesResponse> UpdateAsync(DishesDto dish)
        {
            try
            {
                var exitingdish = await FindDishByIdAsync(dish.Id);
                if (exitingdish == null) return new DishesResponse(false, "Dish already exist");

                var categoryId = await _appDbContext.Category.Where(c => c.CategoryName == dish.categoryName).Select(i => i.CategoryId).FirstOrDefaultAsync();

                exitingdish.dishesName = dish?.dishesName;
                exitingdish.dishesDescription = dish?.dishesDescription;
                exitingdish.categoryId = categoryId;
                exitingdish.dishesPrice = dish.dishesPrice;

                await _appDbContext.SaveChangesAsync();

                return new DishesResponse(true, "Dish update successfully");
            }
            catch (Exception ex)
            {
                return new DishesResponse(false, ex.Message);
            }
        }

        private async Task<Dishes> FindDishByIdAsync(int id) =>
            await _appDbContext.Dish.FirstOrDefaultAsync(u => u.dishesId == id);
    }
}
