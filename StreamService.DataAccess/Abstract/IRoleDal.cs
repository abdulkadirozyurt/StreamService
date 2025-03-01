using StreamService.Core.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Abstract;

public interface IRoleDal : IEntityRepository<Role>
{
    Task<Role> GetByNameAsync(string name);
}
