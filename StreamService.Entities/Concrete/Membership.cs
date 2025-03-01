using StreamService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamService.Entities.Concrete;

public class Membership : BaseEntity
{
    public string Name { get; set; }

    public List<User>? Users { get; set; }
}
