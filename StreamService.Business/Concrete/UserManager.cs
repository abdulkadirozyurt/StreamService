using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using StreamService.Business.Abstract;
using StreamService.Core.Business.Concrete;
using StreamService.Core.DataAccess.Abstract;
using StreamService.Core.Entities.Constants;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.Business.Concrete;

public class UserManager(IUserDal userDal, ITokenGenerator tokenGenerator, IRoleDal roleDal) : EntityManagerBase<User>(userDal), IUserService
{
    private readonly IUserDal _userDal = userDal;
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
    private readonly IRoleDal _roleDal = roleDal;

    public async Task<User> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        var existingUser = await _userDal.GetByEmailAsync(email);
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
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = _passwordHasher.HashPassword(new User(), password),
            Membership = null,
            RoleId = role.Id,
            Role = role,
        };
        await _userDal.CreateAsync(user);

        return user;
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

    public override async Task<List<User>> GetAllAsync()
    {
        var users = await _userDal.GetAllAsync();
        foreach (var user in users)
        {
            user.Role = await _roleDal.GetByIdAsync(user.RoleId);
        }
        return users;
    }

    public override async Task<User> GetByIdAsync(string id)
    {
        var user = await _userDal.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        user.Role = await _roleDal.GetByIdAsync(user.RoleId);
        return user;
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

    public async Task UpdatePasswordAsync(string email, string oldPassword, string newPassword)
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
    }
}
