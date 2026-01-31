using System.Threading;
using System.Threading.Tasks;

namespace MadameCoco.Order.Repositories;

public interface IOrderRepository
{
    Task<Entities.Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Entities.Order order, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
