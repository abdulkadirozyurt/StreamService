using System;
using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.EntityFramework;

public class UserSubscriptionDal(MongoDbContext context) : EntityRepositoryBase<UserSubscription, MongoDbContext>(context), IUserSubscriptionDal
{
    private readonly MongoDbContext _context = context;
}
