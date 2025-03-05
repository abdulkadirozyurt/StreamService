using Microsoft.AspNetCore.Mvc;
using StreamService.Business.Abstract;
using StreamService.Entities.Concrete;

[Route("api/memberships")]
[ApiController]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var memberships = await _subscriptionService.GetAllAsync();
        return Ok(memberships);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var membership = await _subscriptionService.GetByIdAsync(id);
        if (membership == null)
        {
            return NotFound();
        }
        return Ok(membership);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Subscription subscription)
    {
        await _subscriptionService.CreateAsync(subscription);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Subscription subscription)
    {
        if (id != subscription.Id)
        {
            return BadRequest("ID mismatch");
        }
        var updatedMembership = await _subscriptionService.UpdateAsync(subscription);
        return Ok(updatedMembership);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deactivate(string id)
    {
        var result = await _subscriptionService.DeactivateAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
