using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

public class SubscriptionDal(MongoDbContext context) : EntityRepositoryBase<Subscription, MongoDbContext>(context), ISubscriptionDal
{
    private readonly MongoDbContext _context = context;
}
