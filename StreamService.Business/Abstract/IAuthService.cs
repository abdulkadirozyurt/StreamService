using System;
using StreamService.Entities.Dtos.User;

namespace StreamService.Business.Abstract;

public interface IAuthService
{
    Task<UserRegisterDto> RegisterAsync(UserRegisterDto userRegisterDto);
    Task<string> LoginAsync(string email, string password);
    Task<UpdatePasswordDto> UpdatePasswordAsync(string email, string oldPassword, string newPassword);
}
