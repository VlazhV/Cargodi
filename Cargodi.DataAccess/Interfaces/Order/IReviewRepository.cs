using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.DataAccess.Interfaces.Order;

public interface IReviewRepository
{
    Task<List<Review>> GetOrderReviewsAsync(long orderId, CancellationToken cancellationToken);
    Task<Review> CreateOrderReviewAsync(Review review, CancellationToken cancellationToken);
}
