using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete;

public class Subscription : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Duration { get; set; }

    [BsonIgnore]
    public List<User>? Users { get; set; }
}
