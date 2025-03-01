using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete
{
    public class RefreshToken : BaseEntity
    {
        public string? Token { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = default!;
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
