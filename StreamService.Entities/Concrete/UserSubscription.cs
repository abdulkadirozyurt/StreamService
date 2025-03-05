using System;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete;

public class UserSubscription : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate => StartDate.AddDays(DurationInDays);
    public string UserId { get; set; } = default!;
    public string SubscriptionId { get; set; } = default!;

    [BsonIgnore]
    public User? User { get; set; } = default!;

    [BsonIgnore]
    public Subscription Subscription { get; set; } = default!;
    private int DurationInDays { get; set; }

    public void SetDuration(int durationInDays)
    {
        DurationInDays = durationInDays;
    }
}
