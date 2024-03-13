using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Services;
using Talabat.Dtos;
using Talabat.Error;

namespace Talabat.Controllers
{
    [Authorize]
    public class PaymentController : ApiBaseController
    {
        private readonly IPaymentServices paymentServices;

        public PaymentController(IPaymentServices paymentServices)
        {
            this.paymentServices = paymentServices;
        }



        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")]
        public  async Task<ActionResult<CustomerBasketDto>> CreateOrupdatePaymentIntent(string basketId)
        {
            var basket= await paymentServices.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiExResponse(400, "A problem with Your Basket"));

            return Ok(basket);

        }
 
    }
}
