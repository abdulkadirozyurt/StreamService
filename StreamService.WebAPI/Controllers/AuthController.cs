using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamService.Business.Abstract;
using StreamService.Entities.Dtos.User;
using Swashbuckle.AspNetCore.Annotations;

namespace StreamService.WebAPI.Controllers
{
    /// <summary>
    /// Authentication Operations
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [SwaggerTag("Authentication Operations")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, ITokenService tokenService, IUserService userService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register a new user")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(userRegisterDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login a user")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(userLoginDto.Email, userLoginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-password")]
        [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Update user password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                await _authService.UpdatePasswordAsync(updatePasswordDto.Email, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        [SwaggerOperation(Summary = "Refresh access token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var token = _tokenService.GetRefreshToken(refreshToken);

            if (token == null || !token.IsActive)
                return Unauthorized("Invalid or expired refresh token");

            var user = await _userService.GetByIdAsync(token.UserId);
            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken(user);

            _tokenService.RevokeRefreshToken(token);
            _tokenService.SaveRefreshToken(newRefreshToken);

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token });
        }
    }
}
