using System;
using StreamService.Business.Abstract;
using StreamService.Core.Business.Concrete;
using StreamService.Core.DataAccess.Abstract;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.Business.Concrete;

public class UserManager(IUserDal userDal) : EntityManagerBase<User>(userDal), IUserService
{
    private readonly IUserDal _userDal = userDal;
}
