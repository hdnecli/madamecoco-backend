using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MadameCoco.Order.Entities;
using MadameCoco.Order.Features.Orders.Commands.CreateOrder;
using MadameCoco.Order.Repositories;
using MadameCoco.Shared;
using MadameCoco.Shared.Contracts;
using MassTransit;
using Moq;
using Xunit;

namespace MadameCoco.Order.UnitTests;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _mockRepository;
    private readonly Mock<IPublishEndpoint> _mockPublishEndpoint;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _mockRepository = new Mock<IOrderRepository>();
        _mockPublishEndpoint = new Mock<IPublishEndpoint>();
        _handler = new CreateOrderCommandHandler(_mockRepository.Object, _mockPublishEndpoint.Object);
    }

    [Fact]
    public async Task Handle_Should_CreateOrder_And_PublishEvent_When_DataIsValid()
    {
        // Arrange
        var command = new CreateOrderCommand(
            CustomerId: Guid.NewGuid(),
            Address: new Address { AddressLine = "Test St", City = "Test City", Country = "Test Country", CityCode = 1 },
            Items: new List<OrderItemDto>
            {
                new OrderItemDto 
                { 
                    ProductId = Guid.NewGuid(), 
                    Quantity = 1, 
                    UnitPrice = 100, 
                    ProductName = "Test Product",
                    ImageUrl = "http://test.com/img.jpg",
                    Status = "Active"
                }
            }
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);

        // Verify Repository calls
        _mockRepository.Verify(x => x.AddAsync(It.IsAny<Entities.Order>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        // Verify MassTransit publish
        _mockPublishEndpoint.Verify(x => x.Publish<IOrderCreatedEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
