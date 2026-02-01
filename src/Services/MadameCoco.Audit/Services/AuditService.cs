using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MadameCoco.Audit.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MadameCoco.Audit.Services;

/// <summary>
/// Service for managing audit logs in MongoDB.
/// </summary>
public class AuditService
{
    private readonly IMongoCollection<AuditLog> _logsCollection;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditService"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public AuditService(IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString"));
        var mongoDatabase = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName"));
        _logsCollection = mongoDatabase.GetCollection<AuditLog>("AuditLogs");
    }

    /// <summary>
    /// Inserts a new audit log entry asynchronously.
    /// </summary>
    /// <param name="log">The audit log to insert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InsertLogAsync(AuditLog log)
    {
        await _logsCollection.InsertOneAsync(log);
    }

    /// <summary>
    /// Retrieves audit logs created in the last 24 hours.
    /// </summary>
    /// <returns>A list of audit logs.</returns>
    public async Task<List<AuditLog>> GetLogsLast24HoursAsync()
    {
        var filter = Builders<AuditLog>.Filter.Gte(x => x.Timestamp, DateTime.UtcNow.AddHours(-24));
        return await _logsCollection.Find(filter).ToListAsync();
    }
}
