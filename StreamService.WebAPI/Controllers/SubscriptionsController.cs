using Microsoft.AspNetCore.Mvc;
using StreamService.Business.Abstract;
using StreamService.Entities.Concrete;

[Route("api/subscriptions")]
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
        var subscriptions = await _subscriptionService.GetAllAsync();
        return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var subscription = await _subscriptionService.GetByIdAsync(id);
        if (subscription == null)
        {
            return NotFound();
        }
        return Ok(subscription);
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
        var updatedSubscription = await _subscriptionService.UpdateAsync(subscription);
        return Ok(updatedSubscription);
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
