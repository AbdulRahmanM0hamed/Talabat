﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Reopsitory.Date.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(10,2)");
         
            builder.HasOne(P=>P.ProductBrand)
                .WithMany()
                .HasForeignKey(P=>P.ProductBrandId);


            builder.HasOne(P => P.ProductType)
                .WithMany()
                .HasForeignKey(P => P.ProductTypeId);
        }
    }
}
