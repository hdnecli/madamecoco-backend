using System.Threading;
using System.Threading.Tasks;

namespace MadameCoco.Order.Repositories;

public interface IOrderRepository
{
    void Add(Entities.Order order);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
