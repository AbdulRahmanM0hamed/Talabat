using AdminPanalTalabatMVC.Models;
using AutoMapper;
using Talabat.Core.Entities;

namespace AdminPanalTalabatMVC.Helpers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
        }
    }
}
