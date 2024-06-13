using Cargodi.Business.DTOs.Order.Review;

namespace Cargodi.Business.Interfaces.Order;

public interface IReviewService
{
    Task<List<ReviewDto>> GetOrderReviewsAsync(long orderId, CancellationToken cancellationToken);
    Task<ReviewDto> CreateOrderReviewAsync(ReviewDto reviewDto, long orderId, CancellationToken cancellationToken);
}
