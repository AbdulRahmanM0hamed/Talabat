using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;
using Talabat.Dtos;
using Talabat.Error;
using Talabat.Helper;

namespace Talabat.Controllers
{

    public class ProductsController : ApiBaseController
    {
        //private readonly IGenaricRepositort<Product> productRepo;
        //private readonly IGenaricRepositort<ProductBrand> brandRepo;
        //private readonly IGenaricRepositort<ProductType> typeRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
           
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        //[Authorize (AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductReturnToDto>>> GetPorducts([FromQuery]ProductSpecPrams productSpec)
        {
            var spec = new ProductSpecifications(productSpec);
            var Products = await unitOfWork.Repositort<Product>().GetAllWithSpecAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnToDto>>(Products);
            var countSpec = new ProudectWithFilterationForCountSpeceification(productSpec);
            var Count=await unitOfWork.Repositort<Product>().GetCountWithSpecAsync(countSpec);
            return Ok(new Pagenation<ProductReturnToDto>(productSpec.PageIndex, productSpec.PageSize, Count, data));
        }


        [ProducesResponseType(typeof(ProductReturnToDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErorrHandling),StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReturnToDto>> GetPorduct(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await unitOfWork.Repositort<Product>().GetByIdWithSpecAsync(spec);
            if (product == null) return NotFound(new ApiErorrHandling(404));
            var mappedProduct = mapper.Map<Product, ProductReturnToDto>(product);

            return Ok(mappedProduct);

        }

        [HttpGet("Brand")]
        public async Task<ActionResult<IReadOnlyList< ProductBrand>>> GetAllBrand()
        {
            var Brands=await unitOfWork.Repositort<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }

        [HttpGet("Type")]
        public async Task<ActionResult<IReadOnlyList <ProductType>>> GetAllType()
        {
            var type = await unitOfWork.Repositort<ProductType>().GetAllAsync();
            return Ok(type);
        }
    }
}
