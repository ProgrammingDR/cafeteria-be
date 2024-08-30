using Application.DTOs;
using Domine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ISales
    {
        Task<SalesResponse> CreateAsync(CreateSaleDto sale);
        Task<SalesResponse> GetAsync();
        Task<SalesResponse> DeleteAsync(int Id);
        Task<SalesResponse> UpdateAsync(CreateSaleDto sale);
    }
}
