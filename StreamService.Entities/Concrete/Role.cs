using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = default!;

        [BsonIgnore]
        public List<User> Users { get; set; } = new List<User>();
    }
}
