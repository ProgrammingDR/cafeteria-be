using Application.DTOs;
using Domine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IOrders
    {
        Task<OrdersResponse> CreateAsync(OrdersDto order);
        Task<OrdersResponse> GetAsync();
        Task<OrdersResponse> DeleteAsync(int Id);
        Task<OrdersResponse> UpdateAsync(OrdersDto order);
    }
}
