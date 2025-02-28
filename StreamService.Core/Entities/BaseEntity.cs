using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StreamService.Core.Entities;

public abstract class BaseEntity
{
    [Key] 
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)] // MongoDB'de ObjectId olarak kullan�lacak, c#'ta string olarak kullan�lacak
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // SQL i�in ID'nin otomatik artmas�n� sa�lar
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Varsay�lan olarak GUID kullan�l�r

    [BsonElement("createdAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Varsay�lan de�er ekle

    [BsonElement("updatedAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? UpdatedAt { get; set; }
}
