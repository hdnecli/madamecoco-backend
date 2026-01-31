using System;

namespace MadameCoco.Shared.Contracts;

public interface IOrderStatusChangedEvent
{
    public Guid OrderId { get; }
    public string NewStatus { get; }
    public DateTime ChangedAt { get; }
}
