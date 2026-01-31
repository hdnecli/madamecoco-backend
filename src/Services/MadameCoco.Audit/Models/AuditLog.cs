using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MadameCoco.Audit.Models;

public class AuditLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    public Guid OrderId { get; set; }
    public string Action { get; set; } = default!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Details { get; set; } = default!;
}
