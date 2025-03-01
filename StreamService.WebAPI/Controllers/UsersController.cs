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
    private readonly ITokenGenerator _tokenGenerator;

    public UsersController(IUserService userService, ITokenGenerator tokenGenerator)
    {
        _userService = userService;
        _tokenGenerator = tokenGenerator;
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
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        await _userService.DeleteAsync(id);
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
}
