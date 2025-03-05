using StreamService.Business.Abstract;
using StreamService.Core.Business.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;

public class SubscriptionManager(ISubscriptionDal subscriptionDal) : EntityManagerBase<Subscription>(subscriptionDal), ISubscriptionService
{
    private readonly ISubscriptionDal _subscriptionDal = subscriptionDal;
}
