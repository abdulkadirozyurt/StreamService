using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete;

public class Stream : BaseEntity
{
    public string StreamKey { get; set; } = default!;
    public int PortNumber { get; set; }
    public string StreamUrl { get; set; } = default!;
}
