using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MadameCoco.Audit.Models;

/// <summary>
/// Represents an audit log entry in MongoDB.
/// </summary>
public class AuditLog
{
    /// <summary>
    /// Gets or sets the MongoDB unique identifier.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the associated order identifier.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Gets or sets the action performed (e.g., "Created", "Updated").
    /// </summary>
    public string Action { get; set; } = default!;

    /// <summary>
    /// Gets or sets the timestamp of the action.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the detailed payload or description of the action.
    /// </summary>
    public string Details { get; set; } = default!;
}
