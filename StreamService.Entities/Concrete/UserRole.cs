using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete
{
    public class UserRole : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = default!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string RoleId { get; set; } = string.Empty;
        public Role Role { get; set; } = default!;
    }
}
