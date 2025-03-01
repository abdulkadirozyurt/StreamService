using Microsoft.EntityFrameworkCore;
using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.EntityFramework;

public class RoleDal(MongoDbContext context) : EntityRepositoryBase<Role, MongoDbContext>(context), IRoleDal
{
    private readonly MongoDbContext _context = context;

    public async Task<Role> GetByNameAsync(string name)
    {
        return await _context.Roles.Where(r => r.Name == name).FirstOrDefaultAsync();
    }
}
