using StreamService.Business.Abstract;
using StreamService.Core.Business.Concrete;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamService.Business.Concrete;

public class StreamInformationManager(IStreamInformationDal streamInformationDal) : EntityManagerBase<StreamInformation>(streamInformationDal), IStreamInformationService
{
    private readonly IStreamInformationDal _streamInformationDal;
}
