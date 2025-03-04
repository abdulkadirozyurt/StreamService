using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StreamService.Core.Entities;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] // MongoDB'de ObjectId olarak kullanılacak, c#'ta string olarak kullanılacak
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // SQL için ID'nin otomatik artmasını sağlar
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString(); // Varsayılan olarak ObjectId kullanılır

    [BsonElement("createdAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;
}
