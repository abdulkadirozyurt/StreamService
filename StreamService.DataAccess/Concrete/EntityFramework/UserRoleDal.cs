using Microsoft.EntityFrameworkCore;
using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.EntityFramework;

public class UserRoleDal(MongoDbContext context) : EntityRepositoryBase<UserRole, MongoDbContext>(context), IUserRoleDal
{
    private readonly MongoDbContext _context = context;

    public async Task<List<UserRole>> GetByUserIdAsync(string userId)
    {
        // MongoDB.EntityFrameworkCore desteklemediği için Include kullanamıyoruz
        // ToListAsync ile önce verileri çekip sonra manuel birleştirme yapıyoruz
        var userRoles = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .ToListAsync();

        // Her bir UserRole için Role bilgisini manuel olarak yükle
        foreach (var userRole in userRoles)
        {
            userRole.Role = await _context.Roles.FindAsync(userRole.RoleId);
        }

        return userRoles;
    }

    public async Task<List<UserRole>> GetByRoleIdAsync(string roleId)
    {
        // MongoDB.EntityFrameworkCore desteklemediği için Include kullanamıyoruz
        var userRoles = await _context.UserRoles
            .Where(ur => ur.RoleId == roleId)
            .ToListAsync();

        // Her bir UserRole için User bilgisini manuel olarak yükle
        foreach (var userRole in userRoles)
        {
            userRole.User = await _context.Users.FindAsync(userRole.UserId);
        }

        return userRoles;
    }

    public async Task<UserRole> CreateUserRoleAsync(string userId, string roleId)
    {
        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };

        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
        
        return userRole;
    }
}