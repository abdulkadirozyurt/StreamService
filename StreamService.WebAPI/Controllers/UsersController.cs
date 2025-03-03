using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using StreamService.Business.Abstract;
using StreamService.Entities.Concrete;
using StreamService.Entities.Dtos.User;

namespace StreamService.WebAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UsersController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById([FromQuery] string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("Invalid ID format.");
        }
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPut("id")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Update([FromQuery] string id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("Invalid ID format.");
        }
        await _userService.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("Invalid ID format.");
        }
        var result = await _userService.DeactivateAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("deactivate/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate([FromRoute] string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("Invalid ID format.");
        }
        var result = await _userService.DeactivateAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        try
        {
            var user = await _userService.RegisterAsync(
                userRegisterDto.FirstName,
                userRegisterDto.LastName,
                userRegisterDto.Email,
                userRegisterDto.Password
            );
            return Ok(user);
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
            var token = await _userService.LoginAsync(userLoginDto.Email, userLoginDto.Password);
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
            await _userService.UpdatePasswordAsync(updatePasswordDto.Email, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);
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
