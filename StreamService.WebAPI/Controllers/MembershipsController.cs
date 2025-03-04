using Microsoft.AspNetCore.Mvc;
using StreamService.Business.Abstract;
using StreamService.Entities.Concrete;

[Route("api/memberships")]
[ApiController]
public class MembershipsController : ControllerBase
{
    private readonly IMembershipService _membershipService;

    public MembershipsController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var memberships = await _membershipService.GetAllAsync();
        return Ok(memberships);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var membership = await _membershipService.GetByIdAsync(id);
        if (membership == null)
        {
            return NotFound();
        }
        return Ok(membership);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Membership membership)
    {
        var createdMembership = await _membershipService.CreateAsync(membership);
        return CreatedAtAction(nameof(GetById), new { id = createdMembership.Id }, createdMembership);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Membership membership)
    {
        if (id != membership.Id)
        {
            return BadRequest("ID mismatch");
        }
        var updatedMembership = await _membershipService.UpdateAsync(membership);
        return Ok(updatedMembership);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deactivate(string id)
    {
        var result = await _membershipService.DeactivateAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
