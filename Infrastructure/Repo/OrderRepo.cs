using Application.Contracts;
using Application.DTOs;
using Domine.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    internal class OrderRepo : IOrders
    {
        public readonly AppDbContext _appDbContext;

        public OrderRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrdersResponse> CreateAsync(OrdersDto order)
        {
            try
            {
                var exitingOrder = await FindOrderByIdAsync(order.OrdersId);
                if (exitingOrder != null) return new OrdersResponse(false, "Order already exist");

                var dishId = await _appDbContext.Dish.Where(o => o.dishesName == order.dishName).Select(i => i.dishesId).FirstOrDefaultAsync();
                var categoryId = await _appDbContext.Category.Where(c => c.CategoryName == order.categoryName).Select(i => i.CategoryId).FirstOrDefaultAsync();

                var _order = new Orders
                {
                    dishId = dishId,
                    clientId = order.clientId,
                    categoryId = categoryId,
                    cuantity = order.cuantity,
                    state = order.state,
                    orderDate = DateTime.Now,
                };
                _appDbContext.Add(_order);
                await _appDbContext.SaveChangesAsync();

                return new OrdersResponse(true, "Order created successfully");
            }catch (Exception ex)
            {
                return new OrdersResponse(false, ex.Message);
            }
        }

        public async Task<OrdersResponse> DeleteAsync(int Id)
        {
            try
            {
                var exitingOrder = await FindOrderByIdAsync(Id);

                if (exitingOrder == null) return new OrdersResponse(false, "Order not found");

                _appDbContext.Remove(exitingOrder);
                await _appDbContext.SaveChangesAsync();

                return new OrdersResponse(true, "Order deleted successfully");
            }catch(Exception ex)
            {
                return new OrdersResponse(false, ex.Message);
            }
        }

        public async Task<OrdersResponse> GetAsync()
        {
            try
            {
                var OrdersData = await (from order in _appDbContext.Order
                                        join dish in _appDbContext.Dish
                                        on order.dishId equals dish.dishesId
                                        join category in _appDbContext.Category
                                        on dish.categoryId equals category.CategoryId
                                        select new OrdersDto
                                        {
                                            OrdersId = order.Id,
                                            dishName = dish.dishesName!,
                                            categoryName = category.CategoryName,
                                            cuantity = order.cuantity,
                                            state = order.state,
                                            orderDate = order.orderDate,
                                        }).Where(o => o.state != Domine.OrderState.Complete).ToListAsync();

                return new OrdersResponse(true, "Order listed successfully", OrdersData);
            }catch (Exception ex)
            {
                return new OrdersResponse(false, ex.Message);
            }
        }

        public async Task<OrdersResponse> UpdateAsync(OrdersDto order)
        {
            try
            {
                var exitingOrder = await FindOrderByIdAsync(order.OrdersId);
                if (exitingOrder == null) return new OrdersResponse(false, "Order already exist");

                var dishId = await _appDbContext.Dish.Where(o => o.dishesName == order.dishName).Select(i => i.dishesId).FirstOrDefaultAsync();
                var categoryId = await _appDbContext.Category.Where(c => c.CategoryName == order.categoryName).Select(i => i.CategoryId).FirstOrDefaultAsync();

                exitingOrder.dishId = dishId;
                exitingOrder.categoryId = categoryId;
                exitingOrder.state = order.state;
                exitingOrder.orderDate = DateTime.Now;

                await _appDbContext.SaveChangesAsync();

                return new OrdersResponse(true, "Order update successfully");
            }catch(Exception ex)
            {
                return new OrdersResponse(false, ex.Message);
            }
        }

        private async Task<Orders> FindOrderByIdAsync(int id) =>
            await _appDbContext.Order.FirstOrDefaultAsync(u => u.Id == id);
    }
}
