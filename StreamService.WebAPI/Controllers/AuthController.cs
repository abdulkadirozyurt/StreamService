using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamService.Business.Abstract;
using StreamService.Entities.Dtos.User;

namespace StreamService.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
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
