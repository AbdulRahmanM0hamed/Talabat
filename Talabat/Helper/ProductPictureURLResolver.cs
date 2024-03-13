using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Dtos;

namespace Talabat.Helper
{
    public class ProductPictureURLResolver : IValueResolver<Product, ProductReturnToDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureURLResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, ProductReturnToDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiUrlBase"]}{source.PictureUrl}";


            return string.Empty;
        }
    }
}
