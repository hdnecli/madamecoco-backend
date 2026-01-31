using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MadameCoco.Audit.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MadameCoco.Audit.Services;

public class AuditService
{
    private readonly IMongoCollection<AuditLog> _logsCollection;

    public AuditService(IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString"));
        var mongoDatabase = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName"));
        _logsCollection = mongoDatabase.GetCollection<AuditLog>("AuditLogs");
    }

    public async Task InsertLogAsync(AuditLog log)
    {
        await _logsCollection.InsertOneAsync(log);
    }

    public async Task<List<AuditLog>> GetLogsLast24HoursAsync()
    {
        var filter = Builders<AuditLog>.Filter.Gte(x => x.Timestamp, DateTime.UtcNow.AddHours(-24));
        return await _logsCollection.Find(filter).ToListAsync();
    }
}
