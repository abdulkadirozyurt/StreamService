using System;
using Microsoft.EntityFrameworkCore;
using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.EntityFramework;

public class UserDal(MongoDbContext context) : EntityRepositoryBase<User, MongoDbContext>(context), IUserDal
{
    private readonly MongoDbContext _context = context;

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    }

    
}
