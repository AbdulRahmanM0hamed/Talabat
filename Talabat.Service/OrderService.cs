using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specification.Order_Spec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketReopsitort BasketRpo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentServices paymentServices;

        //private readonly IGenaricRepositort<Product> productRepo;
        //private readonly IGenaricRepositort<DeliveryMethod> dMRepo;
        //private readonly IGenaricRepositort<Order> orderRepo;

        public OrderService(IBasketReopsitort BasketRpo, IUnitOfWork unitOfWork,IPaymentServices paymentServices )
        {
            this.BasketRpo = BasketRpo;
            this.unitOfWork = unitOfWork;
            this.paymentServices = paymentServices;
        }
        public async Task<Order?> CreateArderAsync(string buyerEmail, string basketId, Address shippingAddress, int deliveryMethodId)
        {
            //1-Get Basket From basket Repo
            var Basket = await BasketRpo.GetBasketAsync(basketId);
            //2-Get Selected Item At Basket From  productRepo
            var orderItems = new List<OrderItem>();
            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await unitOfWork.Repositort<Product>().GetByIdAsync(item.Id);
                    var productItemOrder = new ProductOrderItem(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }
            //3-Calculate subTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            //4-Get Delivery Method From DM Repo
            var deliveryMethod = await unitOfWork.Repositort<DeliveryMethod>().GetByIdAsync(deliveryMethodId);


            //5-Create Order
            var spec = new orderWithPaymentSpec(Basket.PaymentIntentId);
            var exsistOrder = await unitOfWork.Repositort<Order>().GetByIdWithSpecAsync(spec);
            if(exsistOrder is not null)
            {
                unitOfWork.Repositort<Order>().delete(exsistOrder);
                await paymentServices.CreateOrUpdatePaymentIntent(basketId);
            }

            var order = new Order(buyerEmail, shippingAddress, Basket.PaymentIntentId, deliveryMethod, orderItems, subTotal);
            //6-Add Order Locally
            await unitOfWork.Repositort<Order>().Add(order);
            //7-Save Order In Date Base 
            var Result = await unitOfWork.Complete();
            if (Result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders = await unitOfWork.Repositort<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<Order> GetOrderIDForUserAsync(int OrderId, string buyerEmail)
        {
            var spec = new OrderSpecification(OrderId, buyerEmail);
            var order = await unitOfWork.Repositort<Order>().GetByIdWithSpecAsync(spec);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod = await unitOfWork.Repositort<DeliveryMethod>().GetAllAsync();
            return deliveryMethod;
        }
    }
}
