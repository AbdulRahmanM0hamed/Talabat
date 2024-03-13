using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Dtos;
using Talabat.Error;
using Talabat.Reopsitory;

namespace Talabat.Controllers
{
    public class BacketController : ApiBaseController
    {
        private readonly IBasketReopsitort basketReopsitort;
        private readonly IMapper mapper;

        public BacketController(IBasketReopsitort basketReopsitort ,IMapper mapper)
        {
            this.basketReopsitort = basketReopsitort;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetCustmerBasket(string customerId)
        {
            var basket = await basketReopsitort.GetBasketAsync(customerId);
            return basket is null ? new CustomerBasket(customerId) : basket;
        }


        [HttpPost("UpdateOrCreate")]
        public async Task<ActionResult<CustomerBasketDto>> UpdateOrCreate(CustomerBasketDto basket)
        {
            var BasketMap=mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdated = await basketReopsitort.UpDateBasketAsync(BasketMap);

            if (createdOrUpdated is null)
                return BadRequest(new ApiErorrHandling(400));

            return Ok(createdOrUpdated);
        }



        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await basketReopsitort.DeleteBasketAsync(id);
             
        }

    }
}
