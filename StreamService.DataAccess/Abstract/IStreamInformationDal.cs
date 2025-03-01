using System;
using StreamService.Core.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Abstract;

public interface IStreamInformationDal : IEntityRepository<StreamInformation> { }
