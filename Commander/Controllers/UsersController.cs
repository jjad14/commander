using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // Login
        // [HttpPost("login")]
        // public async Task<ActionResult<UserToReturnDto>> Login(UserLoginDto userLoginDto)
        // {
        //     var user = await _userManager.FindByEmailAsync(userLoginDto.Email.ToLower());

        //     if (user == null)
        //     {
        //         return Unauthorized(new ApiResponse(401));
        //     }

        //     var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

        //     if (!result.Succeeded)
        //     {
        //         return Unauthorized(new ApiResponse(401));
        //     }

        //     return new UserToReturnDto
        //     {
        //         DisplayName = user.DisplayName,
        //         Email = user.Email,
        //         Token = await _tokenService.CreateToken(user)
        //     };
        // }

        // // Register
        // [HttpPost("register")]
        // public async Task<ActionResult<UserToReturnDto>> Register(UserRegisterDto userRegisterDto)
        // {
        //     if (CheckEmailExists(userRegisterDto.Email).Result.Value) {
        //         return new BadRequestObjectResult(new ApiValidationErrorResponse{
        //             Errors = new []{"Email Address already exists."}
        //             });
        //     }

        //     var user = new User
        //     {
        //         DisplayName = userRegisterDto.DisplayName,
        //         Email = userRegisterDto.Email.ToLower(),
        //         UserName = userRegisterDto.Email.ToLower()
        //     };

        //     var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

        //     if (!result.Succeeded)
        //     {
        //         return BadRequest(new ApiResponse(400));
        //     }

        //     var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

        //     if (!roleResult.Succeeded) 
        //     {
        //         return BadRequest(new ApiResponse(400));
        //     }

        //     return new UserToReturnDto
        //     {
        //         DisplayName = user.DisplayName,
        //         Email = user.Email,
        //         Token = await _tokenService.CreateToken(user)
        //     };
        // }
        // // Get User
        // [Authorize]
        // [HttpGet]
        // public async Task<ActionResult<UserToReturnDto>> GetCurrentUser()
        // {
        //     var user = await _userManager.FindByEmailFromClaimsPrincipal(HttpContext.User);

        //     return new UserToReturnDto
        //     {
        //         DisplayName = user.DisplayName,
        //         Email = user.Email,
        //         Token = await _tokenService.CreateToken(user)
        //     };
        // }

        // // Check if email exists
        // [HttpGet("emailverify")]
        // public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
        // {
        //     return await _userManager.FindByEmailAsync(email.ToLower()) != null;
        // }

    }
}
