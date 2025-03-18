using System;
using StreamService.Business.Abstract;
using StreamService.Core.Business.Abstract;
using StreamService.Core.Business.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.Business.Concrete;

public class UserSubscriptionManager(IUserSubscriptionDal userSubscriptionDal)
    : EntityManagerBase<UserSubscription>(userSubscriptionDal),
        IUserSubscriptionService
{
    private readonly IUserSubscriptionDal _userSubscriptionDal = userSubscriptionDal;
}
