using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Core.Specification.Order_Spec
{
    public class orderWithPaymentSpec : BaseSpecification<Order>
    {

        public orderWithPaymentSpec(string IntentId) : base(O => O.PaymentIntentId == IntentId)
        {

        }
    }
}
