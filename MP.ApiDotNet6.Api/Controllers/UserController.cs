using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs.User;
using MP.ApiDotNet6.Application.Services.Interface;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> PostAsync([FromForm] UserDTO userDTO)
        {
            var result = await _userService.GenerateTokenAsync(userDTO);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result);
        }
    }
}
