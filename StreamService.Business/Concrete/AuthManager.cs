using System;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using StreamService.Business.Abstract;
using StreamService.Core.Entities.Constants;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;
using StreamService.Entities.Dtos.User;

namespace StreamService.Business.Concrete;

public class AuthManager(IUserDal userDal, PasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator, IRoleDal roleDal) : IAuthService
{
    private readonly IUserDal _userDal = userDal;
    private readonly PasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
    private readonly IRoleDal _roleDal = roleDal;

    public async Task<UserRegisterDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var existingUser = await _userDal.GetByEmailAsync(userRegisterDto.Email);
        if (existingUser != null)
        {
            throw new Exception("Email already exists.");
        }

        var role = await _roleDal.GetByNameAsync(UserRoleConstants.User);
        if (role == null)
        {
            throw new Exception("Role 'User' not found.");
        }

        var user = new User
        {
            Id = ObjectId.GenerateNewId().ToString(),
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userRegisterDto.Email,
            Password = _passwordHasher.HashPassword(new User(), userRegisterDto.Password),
            Subscription = null,
            RoleId = role.Id,
            Role = role,
        };
        await _userDal.CreateAsync(user);

        return new UserRegisterDto { FirstName = user.FirstName, LastName = user.LastName };
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var isPasswordValid = await ValidatePasswordAsync(email, password);
        if (!isPasswordValid)
        {
            throw new Exception("Invalid password.");
        }

        var user = await _userDal.GetByEmailAsync(email);
        user.Role = await _roleDal.GetByIdAsync(user.RoleId);

        var token = _tokenGenerator.GenerateToken(user);

        return token;
    }

    public async Task<UpdatePasswordDto> UpdatePasswordAsync(string email, string oldPassword, string newPassword)
    {
        var user = await _userDal.GetByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, oldPassword);
        if (result != PasswordVerificationResult.Success)
        {
            throw new Exception("Old password is incorrect.");
        }

        user.Password = _passwordHasher.HashPassword(user, newPassword);
        await _userDal.UpdateAsync(user);

        return new UpdatePasswordDto
        {
            Email = email,
            OldPassword = oldPassword,
            NewPassword = newPassword,
        };
    }

    public async Task<bool> ValidatePasswordAsync(string email, string password)
    {
        var user = await _userDal.GetByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        return result == PasswordVerificationResult.Success;
    }
}
