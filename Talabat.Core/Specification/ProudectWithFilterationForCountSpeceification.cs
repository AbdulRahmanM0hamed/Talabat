using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class ProudectWithFilterationForCountSpeceification : BaseSpecification<Product>
    {
        public ProudectWithFilterationForCountSpeceification(ProductSpecPrams productSpec)
            : base(P =>
            (string.IsNullOrEmpty(productSpec.SearchByName) || P.Name.ToLower().Contains(productSpec.SearchByName)) &&
            (!productSpec.BrandId.HasValue || P.ProductBrandId == productSpec.BrandId) &&
            (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId))
        {
           
        }
    }
}

