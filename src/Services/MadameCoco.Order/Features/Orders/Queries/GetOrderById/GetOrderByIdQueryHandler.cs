using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MadameCoco.Shared;
using MadameCoco.Shared.Contracts;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MadameCoco.Order.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderDto>>
{
    private readonly IConfiguration _configuration;

    public GetOrderByIdQueryHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Response<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        
            var sql = @"
            SELECT 
                o.""Id"", o.""CustomerId"", o.""Status"", o.""CreatedAt"",
                o.""AddressLine"", o.""City"", o.""Country"", o.""CityCode"",
                i.""Id"", i.""ProductId"", i.""ProductName"", i.""ImageUrl"", i.""Status"", i.""UnitPrice"", i.""Quantity""
            FROM ""Orders"" o
            LEFT JOIN ""OrderItems"" i ON o.""Id"" = i.""OrderId""
            WHERE o.""Id"" = @Id";

        var orderDictionary = new Dictionary<Guid, OrderDto>();

        var result = await connection.QueryAsync<OrderDto, AddressDto, OrderItemDto, OrderDto>(
            sql,
            (order, address, item) =>
            {
                if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
                {
                    currentOrder = order;
                    currentOrder.Address = address;
                    currentOrder.Items = new List<OrderItemDto>();
                    orderDictionary.Add(currentOrder.Id, currentOrder);
                }

                if (item != null)
                {
                    currentOrder.Items.Add(item);
                }

                return currentOrder;
            },
            new { Id = request.Id },
            splitOn: "AddressLine,Id"
        );

        var orderDto = orderDictionary.Values.FirstOrDefault();

        if (orderDto == null)
        {
            return Response<OrderDto>.Fail("Order not found", 404);
        }

        return Response<OrderDto>.Success(orderDto, 200);
    }
}
