using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.identity;
using Talabat.Core.Services;
using Talabat.Dtos;
using Talabat.Error;
using Talabat.Extentions;
using Talabat.Service;

namespace Talabat.Controllers
{

    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signIn;
        private readonly ITokenService token;
        private readonly IMapper mapper;

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signIn,
            ITokenService token,
            IMapper mapper
            )

        {
            this.userManager = userManager;
            this.signIn = signIn;
            this.token = token;
            this.mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiErorrHandling(401));
            var result = await signIn.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiErorrHandling(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await token.CreateTokenAsync(user, userManager)
            }); ;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> register(RegisterDto model)
        {
            if(CheckEmailExsist(model.Email).Result.Value)
                return BadRequest(new ApiValidtionErorrResponse() {Erorrs =new string[] {"This Email is Already Exsist "} });
            //try 


            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiErorrHandling(400));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await token.CreateTokenAsync(user, userManager)

            });
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await token.CreateTokenAsync(user, userManager)
            });

        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            var address = mapper.Map<Address, AddressDto>(user.Addess);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpDateUserAddress(AddressDto UpdateAddress)
        {
            var address = mapper.Map<AddressDto, Address>(UpdateAddress);
            var user = await userManager.FindUserWithAddressByEmailAsync(User);

            address.Id = user.Addess.Id;

            user.Addess = address;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiErorrHandling(400));

            return Ok(UpdateAddress);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmailExsist(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;//True
        }


    }
}
