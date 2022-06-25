using MobileStore.Domain.Entities;
using MobileStore.Order.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Order.Domain.Service
{
    public interface IOrderService
    {
        Task<OrderResponse<IEnumerable<Ordering>>> GetAllOrders();
        Task<OrderResponse<Ordering>> CreateOrder(Ordering order);
    }
}
