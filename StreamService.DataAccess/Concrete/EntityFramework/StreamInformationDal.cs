using System;
using StreamService.Core.DataAccess.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.EntityFramework;

public class StreamInformationDal(MongoDbContext context)
    : EntityRepositoryBase<StreamInformation, MongoDbContext>(context),
        IStreamInformationDal { }
