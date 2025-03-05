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

    public UsersController(IUserService userService, ITokenService tokenService, IAuthService authService)
    {
        _userService = userService;
    }

    [HttpGet]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("id")]
    // [Authorize(Roles = "Admin")]
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
    // [Authorize(Roles = "Admin,User")]
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

    [HttpPut("deactivate/{id}")]
    // [Authorize(Roles = "Admin")]
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
}
