using System;

namespace MadameCoco.Shared.Contracts;

/// <summary>
/// Integration event raised when an order's status is changed.
/// </summary>
public interface IOrderStatusChangedEvent
{
    /// <summary>
    /// Gets the unique identifier of the order.
    /// </summary>
    public Guid OrderId { get; }

    /// <summary>
    /// Gets the new status of the order.
    /// </summary>
    public string NewStatus { get; }

    /// <summary>
    /// Gets the timestamp when the status change occurred.
    /// </summary>
    public DateTime ChangedAt { get; }
}
