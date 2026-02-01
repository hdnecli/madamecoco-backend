using System;
using System.Text.Json;
using System.Threading.Tasks;
using MadameCoco.Audit.Models;
using MadameCoco.Audit.Services;
using MadameCoco.Shared.Contracts;
using MassTransit;

namespace MadameCoco.Audit.Consumers;

/// <summary>
/// Consumes the OrderCreated event to generate audit logs.
/// </summary>
public class OrderCreatedConsumer : IConsumer<IOrderCreatedEvent>
{
    private readonly AuditService _auditService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderCreatedConsumer"/> class.
    /// </summary>
    /// <param name="auditService">The audit service.</param>
    public OrderCreatedConsumer(AuditService auditService)
    {
        _auditService = auditService;
    }

    /// <summary>
    /// Consumes the message and inserts an audit log.
    /// </summary>
    /// <param name="context">The consume context.</param>
    /// <returns>A task representing the operation.</returns>
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var message = context.Message;
        
        var auditLog = new AuditLog
        {
            OrderId = message.OrderId,
            Action = "Created",
            Timestamp = DateTime.UtcNow,
            Details = JsonSerializer.Serialize(message)
        };

        await _auditService.InsertLogAsync(auditLog);

        Console.WriteLine($"Order {message.OrderId} audited successfully.");
    }
}
