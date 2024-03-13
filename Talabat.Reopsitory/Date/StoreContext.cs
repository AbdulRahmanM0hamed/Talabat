using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Reopsitory.Date.Configurations;
using Order = Talabat.Core.Entities.Order_Aggregation.Order;

namespace Talabat.Reopsitory.Date
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
             



        public DbSet<Product> products { get; set; }

        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem>ordersItems { get; set; } 
        public DbSet<DeliveryMethod> deliveryMethods { get; set; } 






    }
}
