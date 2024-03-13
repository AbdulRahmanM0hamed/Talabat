using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration configuration;
        private readonly IBasketReopsitort basketReopsitort;
        private readonly IUnitOfWork unitOfWork;

        public PaymentServices(IConfiguration configuration
            , IBasketReopsitort basketReopsitort
            , IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketReopsitort = basketReopsitort;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            var basket = await basketReopsitort.GetBasketAsync(basketId);
            if (basket is null) return null;
            var ShippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repositort<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingPrice = deliveryMethod.Cost;
                basket.SippingCost = deliveryMethod.Cost;
            }

            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.Repositort<Product>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)ShippingPrice *100 ,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                  Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)ShippingPrice *100 

                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);

            }
            await basketReopsitort.UpDateBasketAsync(basket);
            return basket;

        }
    }
}
