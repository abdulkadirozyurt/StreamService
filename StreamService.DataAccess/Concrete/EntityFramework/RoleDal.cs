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
        var role = await _context.Roles.Where(role => role.Name.ToString() == name).FirstOrDefaultAsync();
        if (role == null)
        {
            // Log the error for debugging
            Console.WriteLine($"Role not found: {name}");
            // Hata fırlatmak yerine null döndür
            return null;
        }
        return role;
    }

}
