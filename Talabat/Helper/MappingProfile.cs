using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Dtos;

namespace Talabat.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductReturnToDto>()
                .ForMember(PD => PD.ProductType, O => O.MapFrom(P => P.ProductType.Name))
                .ForMember(PD => PD.ProductBrand, O => O.MapFrom(P => P.ProductBrand.Name))
                .ForMember(PD => PD.PictureUrl, O => O.MapFrom<ProductPictureURLResolver>());

            CreateMap<Talabat.Core.Entities.identity.Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto, Talabat.Core.Entities.Order_Aggregation.Address>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
           

        }
    }
}
