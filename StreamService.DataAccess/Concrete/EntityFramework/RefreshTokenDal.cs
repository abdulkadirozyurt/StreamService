using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.EntityFramework
{
    public class RefreshTokenDal(MongoDbContext context) : EntityRepositoryBase<RefreshToken, MongoDbContext>(context), IRefreshTokenDal
    {
        private readonly MongoDbContext _context = context;

        public RefreshToken GetByToken(string token)
        {
            return _context.RefreshTokens.Where((RefreshToken x) => x.Token == token).FirstOrDefault()!;
        }
    }
}
