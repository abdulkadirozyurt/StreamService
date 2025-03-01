using StreamService.Core.DataAccess.Abstract;
using StreamService.Core.Entities;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Abstract
{
    public interface IRefreshTokenDal : IEntityRepository<RefreshToken>
    {
        RefreshToken GetByToken(string token);
    }
}
