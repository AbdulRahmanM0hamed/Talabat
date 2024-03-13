using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    public class Order : BaseEntities
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress,string IntentId, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            PaymentIntentId = IntentId;
            DeliveryMethod = deliveryMethod;
            this.items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }
        public DeliveryMethod  DeliveryMethod { get; set; }

        public ICollection<OrderItem > items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } 



    }
}
