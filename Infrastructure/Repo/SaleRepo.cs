using Application.Contracts;
using Application.DTOs;
using Domine;
using Domine.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    internal class SaleRepo : ISales
    {
        private readonly AppDbContext _appDbContext;

        public SaleRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<SalesResponse> CreateAsync(CreateSaleDto saleDto)
        {
            try
            {
                var saleExist = await FindSaleByIdAsync(saleDto.salesId);

                if (saleExist != null) return new SalesResponse(false, "La venta ya existe");

        
                var existingOrder = await _appDbContext.Order
                    .Where(o => o.Id == saleDto.orderId && o.state == OrderState.Send)
                    .FirstOrDefaultAsync();

                if (existingOrder == null) return new SalesResponse(false, "Orden no encontrada.");

                var clientName = await _appDbContext.Users
                    .Where(u => u.Id == existingOrder.clientId)
                    .Select(u => u.Name)
                    .FirstOrDefaultAsync();

                if (clientName == null) return new SalesResponse(false, "Cliente no encontrado");
                
                var newSale = new Sales
                {
                    OrderID = existingOrder.Id, 
                    employeeId = saleDto.employeeId,
                    clientName = clientName,
                    saleDate = saleDto.saleDate
                };

                _appDbContext.Sale.Add(newSale);
                await _appDbContext.SaveChangesAsync();

                existingOrder.state = OrderState.Complete;
                await _appDbContext.SaveChangesAsync();

                return new SalesResponse(true, "Venta agregada exitosamente.");
            }
            catch (Exception ex)
            {
                return new SalesResponse(false, $"Error al agregar la venta: {ex.Message}");
            }
        }


        public async Task<SalesResponse> DeleteAsync(int Id)
        {
            var saleExist = await FindSaleByIdAsync(Id!);
            if (saleExist == null) return new SalesResponse(false, "La categoria ya existe");

             _appDbContext.Remove(saleExist);
            await _appDbContext.SaveChangesAsync();

            return new SalesResponse(true, "Categoria agregada");

        }

        public async Task<SalesResponse> GetAsync()
        {
            try
            {
                var salesData = await (from sale in _appDbContext.Sale
                                       join order in _appDbContext.Order
                                       on sale.OrderID equals order.Id
                                       join dish in _appDbContext.Dish
                                       on order.dishId equals dish.dishesId
                                       join category in _appDbContext.Category
                                       on order.categoryId equals category.CategoryId
                                       join userc in _appDbContext.Users
                                       on order.clientId equals userc.Id
                                       join usere in _appDbContext.Users
                                       on sale.employeeId equals usere.Id
                                       select new SaleDto
                                       {
                                           salesId = sale.Id,
                                           dishName = dish.dishesName,
                                           categoryName = category.CategoryName,
                                           clientName = userc.Name,
                                           employeeName = usere.Name,
                                           cuantity = order.cuantity,
                                           price = (dish.dishesPrice * order.cuantity),
                                           saleDate = sale.saleDate
                                       }).ToListAsync();


                return new SalesResponse(true, "Clients list retrieved successfully", salesData);
            }
            catch (Exception ex)
            {
                return new SalesResponse(false, ex.Message);
            }
        }

        public async Task<SalesResponse> UpdateAsync(CreateSaleDto saleDto)
        {
            try
            {
                var saleToEdit = await FindSaleByIdAsync(saleDto.salesId);

                var existingOrder = await _appDbContext.Order
                    .Where(o => o.Id == saleDto.orderId && o.state == OrderState.Send)
                    .FirstOrDefaultAsync();

                if (existingOrder == null) return new SalesResponse(false, "Orden no encontrada.");

                saleToEdit.OrderID = existingOrder.Id;
                saleToEdit.employeeId = saleDto.employeeId;
                saleToEdit.saleDate = saleDto.saleDate;

                await _appDbContext.SaveChangesAsync();

                return new SalesResponse(true, "Datos modificados");
            }
            catch (Exception ex)
            {
                return new SalesResponse(false, $"An error occurred while updating the client: {ex.Message}");
            }
        }

        private async Task<Sales> FindSaleByIdAsync(int id) =>
            await _appDbContext.Sale.FirstOrDefaultAsync(u => u.Id == id);
    }
}
