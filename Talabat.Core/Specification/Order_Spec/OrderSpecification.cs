using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Core.Specification.Order_Spec
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string email)
            :base(O=>O.BuyerEmail ==email)
        {
            IncludeS.Add(O => O.DeliveryMethod);
            IncludeS.Add(O => O.items);

            AddOrderByDescending(O => O.orderDate);
        }

        public OrderSpecification(int id, string email )
           : base(O => O.BuyerEmail == email&& O.Id ==id)
        {
            IncludeS.Add(O => O.DeliveryMethod);
            IncludeS.Add(O => O.items);

           
        }
    }
}
