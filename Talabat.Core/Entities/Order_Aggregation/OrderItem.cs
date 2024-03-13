using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    public class OrderItem : BaseEntities
    {
        public OrderItem()
        {

        }

        public OrderItem(ProductOrderItem product, decimal price, int quantity)
        {
            this.product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductOrderItem product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }



    }
}
