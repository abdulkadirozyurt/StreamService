using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete;

[Index(nameof(IngestKey), IsUnique = true)]
[Index(nameof(WatchKey), IsUnique = true)]
public class StreamInformation : BaseEntity
{
    public string HostAddress { get; set; } = default!;
    public string IngestKey { get; set; } = default!;
    public string WatchKey { get; set; } = default!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = default!;

    [BsonIgnore]
    public User User { get; set; }

    // Hesaplanmış özellikler ile formatı dinamik üretelim.
    [BsonIgnore] // <path>
    // public string ComputedIngestUrl => $"srt://{HostAddress}:4000?streamid=publish:{User.NickName}&passphrase={PublishKey}";
    public string ComputedIngestUrl => $"srt://{HostAddress}:4000?streamid={IngestKey}";

    [BsonIgnore]
    // public string ComputedWatchUrl => $"srt://{HostAddress}:4000?streamid=read:{User.NickName}&passphrase={WatchKey}";
    public string ComputedWatchUrl => $"srt://{HostAddress}:4000?streamid={WatchKey}";
}
