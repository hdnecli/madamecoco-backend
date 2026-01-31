using System.Threading;
using System.Threading.Tasks;
using MadameCoco.Order.Data;

namespace MadameCoco.Order.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public void Add(Entities.Order order)
    {
        _context.Orders.Add(order);
    }

    public async Task<Entities.Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Orders.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task AddAsync(Entities.Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
