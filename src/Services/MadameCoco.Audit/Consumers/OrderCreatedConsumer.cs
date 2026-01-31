using System;
using System.Text.Json;
using System.Threading.Tasks;
using MadameCoco.Audit.Models;
using MadameCoco.Audit.Services;
using MadameCoco.Shared.Contracts;
using MassTransit;

namespace MadameCoco.Audit.Consumers;

public class OrderCreatedConsumer : IConsumer<IOrderCreatedEvent>
{
    private readonly AuditService _auditService;

    public OrderCreatedConsumer(AuditService auditService)
    {
        _auditService = auditService;
    }

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
