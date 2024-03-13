using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    public class DeliveryMethod :BaseEntities
    {
        public DeliveryMethod()
        {

        }
        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        // int  => ID
        public string ShortName { get; set; }
        public string Description { get; set; }

        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
       
    }
}
