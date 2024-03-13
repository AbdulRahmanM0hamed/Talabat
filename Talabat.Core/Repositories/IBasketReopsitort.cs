using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories
{
    public interface IBasketReopsitort
    {
         Task<CustomerBasket> GetBasketAsync(string BasketID); 
         Task<CustomerBasket> UpDateBasketAsync(CustomerBasket BasketID); 
         Task<bool> DeleteBasketAsync(string BasketID); 
    }
}
