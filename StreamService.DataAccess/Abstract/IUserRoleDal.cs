using StreamService.Core.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Abstract;

public interface IUserRoleDal : IEntityRepository<UserRole>
{
    Task<List<UserRole>> GetByUserIdAsync(string userId);
    Task<List<UserRole>> GetByRoleIdAsync(string roleId);
    Task<UserRole> CreateUserRoleAsync(string userId, string roleId);
}
