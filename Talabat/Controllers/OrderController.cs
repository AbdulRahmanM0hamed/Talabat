using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Services;
using Talabat.Core.Specification.Order_Spec;
using Talabat.Dtos;
using Talabat.Error;

namespace Talabat.Controllers
{
    [Authorize]
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }


        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = mapper.Map<AddressDto, Address>(orderDto.ShoppingAddress);
            var order = await orderService.CreateArderAsync(buyerEmail, orderDto.BasketId, address, orderDto.DeliveryMethod);

            if (order is null) return BadRequest(new ApiExResponse(400));

            return Ok(order);
        }

        [HttpGet("GetOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetOrderForUserAsync(buyerEmail);
            return Ok(orders);
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExResponse), StatusCodes.Status404NotFound)]

        [HttpGet("GetOrderByIdForUser/{Id}")]
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderIDForUserAsync(id, buyerEmail);
            return Ok(order);
        }


        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var deliveryMethod = await orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethod);

        }


    }
}
