using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateArderAsync(string buyerEmail,string basketId, Address shippingAddress, int deliveryMethod);

        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);

        Task<Order> GetOrderIDForUserAsync(int OrderId,string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
