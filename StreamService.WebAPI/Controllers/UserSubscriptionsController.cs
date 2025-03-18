using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamService.Business.Abstract;

namespace StreamService.WebAPI.Controllers
{
    [Route("api/user-subscriptions")]
    [ApiController]
    public class UserSubscriptionsController(IUserSubscriptionService userSubscriptionService) : ControllerBase
    {
        private readonly IUserSubscriptionService _userSubscriptionService = userSubscriptionService;


        
    }
}
