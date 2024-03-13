using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Reopsitory
{
    public class BasketRepositortL : IBasketReopsitort
    {
        private readonly IDatabase _database;
        public BasketRepositortL(IConnectionMultiplexer redis)
        {
            this._database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketID)
        {
            return await _database.KeyDeleteAsync(BasketID);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketID)
        {
            var Basket = await _database.StringGetAsync(BasketID);

            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket> UpDateBasketAsync(CustomerBasket Basket)
        {
            var CreateOrUpDate = await _database.StringSetAsync(Basket.Id,JsonSerializer.Serialize(Basket),TimeSpan.FromDays(1));

            if (!CreateOrUpDate) return null;
            return await GetBasketAsync(Basket.Id);

        }
    }
}
