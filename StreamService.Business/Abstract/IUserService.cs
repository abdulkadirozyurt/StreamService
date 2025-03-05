using System;
using StreamService.Core.Business.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.Business.Abstract;

public interface IUserService : IEntityService<User>
{
    // Task<User> RegisterAsync(string firstName, string lastName, string email, string password);
    // Task<string> LoginAsync(string email, string password);
    // Task UpdatePasswordAsync(string email, string oldPassword, string newPassword);
}
