using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete;

public class Membership : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public TimeSpan Duration { get; set; }

    [BsonIgnore]
    public List<User>? Users { get; set; }
}
