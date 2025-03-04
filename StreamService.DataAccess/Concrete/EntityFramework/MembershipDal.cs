using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

public class MembershipDal(MongoDbContext context) : EntityRepositoryBase<Membership, MongoDbContext>(context), IMembershipDal
{
    private readonly MongoDbContext _context = context;
}
