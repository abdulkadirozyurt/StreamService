using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;
using StreamService.Entities.Enums;

namespace StreamService.Entities.Concrete
{
    public class Role : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        public RoleType Name { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
