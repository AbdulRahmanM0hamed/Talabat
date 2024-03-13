using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Reopsitory.Date
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.productBrands.Any())
            { 
                var brandsDate = File.ReadAllText("../Talabat.Reopsitory/Date/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsDate);

                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await dbContext.productBrands.AddAsync(brand);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            if (!dbContext.productTypes.Any())
            {
                var TypesDate = File.ReadAllText("../Talabat.Reopsitory/Date/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesDate);

                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbContext.productTypes.AddAsync(type);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            if (!dbContext.products.Any())
            {
                var productsDate = File.ReadAllText("../Talabat.Reopsitory/Date/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsDate);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await dbContext.products.AddAsync(product);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            if (!dbContext.deliveryMethods.Any())
            {
                var MethodDate = File.ReadAllText("../Talabat.Reopsitory/Date/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(MethodDate);

                if (deliveryMethods?.Count > 0)
                {
                    foreach (var delivery in deliveryMethods)
                    {
                        await dbContext.deliveryMethods.AddAsync(delivery);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }


        }
    }
}
