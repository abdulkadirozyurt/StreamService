using System;
using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.EntityFramework;

public class UserDal(MongoDbContext context) : EntityRepositoryBase<User, MongoDbContext>(context), IUserDal { }
