using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete;

[Index(nameof(IngestKey), IsUnique = true)]
[Index(nameof(WatchKey), IsUnique = true)]
public class StreamInformation : BaseEntity
{
    public string IngestKey { get; set; } = default!;
    public string WatchKey { get; set; } = default!;
    public int IngestPort { get; set; }
    public int WatchPort { get; set; }
    public string IngestUrl { get; set; } = default!;
    public string WatchUrl { get; set; } = default!;

    public User User { get; set; }
}
