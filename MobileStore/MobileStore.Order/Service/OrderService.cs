using Microsoft.EntityFrameworkCore;
using MobileStore.Domain.Entities;
using MobileStore.Order.Domain.Data;
using MobileStore.Order.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Order.Domain.Service
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _db;

        public OrderService(DataContext db) => _db = db;

        public async Task<OrderResponse<Ordering>> CreateOrder(Ordering order)
        {
            var response = new OrderResponse<Ordering>();

            try
            {
                await _db.AddAsync(order);
                await _db.SaveChangesAsync();
                response.Description = "Заявка на заказ успешна добавлена";
                response.Data = order;
            }
            catch (Exception ex)
            {
                return new OrderResponse<Ordering>() { Description = $"Ошибка {ex.Message}" };
            }
            return response;
        }


        public async Task<OrderResponse<IEnumerable<Ordering>>> GetAllOrders()
        {
            var response = new OrderResponse<IEnumerable<Ordering>>();

            try
            {
                response.Data = await _db.Orders.ToListAsync();
                response.Description = "Успешный запрос";
                return response;
            }
            catch(Exception ex)
            {
                return new OrderResponse<IEnumerable<Ordering>> {Description = $"Ошибка запроса {ex.Message}" };
            }
        }
    }
}
