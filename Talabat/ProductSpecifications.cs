using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat
{
    public class ProductSpecifications : BaseSpecification<Product>
    {
        public ProductSpecifications(ProductSpecPrams productSpec)
            :base(P=>
            (string.IsNullOrEmpty(productSpec.SearchByName)||P.Name.ToLower().Contains(productSpec.SearchByName))&&
            (!productSpec.BrandId.HasValue|| P.ProductBrandId== productSpec.BrandId) &&
            (!productSpec.TypeId.HasValue|| P.ProductTypeId== productSpec.TypeId) )
        {
            IncludeS.Add(P => P.ProductBrand);
            IncludeS.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(productSpec.sort))
            {
                switch (productSpec.sort.ToLower())
                {
                    case"priceasc":
                      AddOrderBy(P=>P.Price);
                        break;
                    case "pricedsc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }

            ApplyPagination(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
        }
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            IncludeS.Add(P => P.ProductBrand);
            IncludeS.Add(P => P.ProductType);

        }
    }
}
