using AutoMapper;
using Cargodi.Business.DTOs.Order.Review;
using Cargodi.Business.Interfaces.Order;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Interfaces.Order;

namespace Cargodi.Business.Services.Order;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    
    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    
    public async Task<ReviewDto> CreateOrderReviewAsync(ReviewDto reviewDto, long orderId, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            Description = reviewDto.Description,
            Rating = reviewDto.Rating,
            OrderId = orderId
        };

        review = await _reviewRepository.CreateOrderReviewAsync(review, cancellationToken);

        return new ReviewDto
        {
            Description = review.Description,
            Rating = review.Rating
        };
    }

    public async Task<List<ReviewDto>> GetOrderReviewsAsync(long orderId, CancellationToken cancellationToken)
    {
        var reviews = await _reviewRepository.GetOrderReviewsAsync(orderId, cancellationToken);

        var reviewDtos = reviews.ConvertAll(r => new ReviewDto
        {
            Description = r.Description,
            Rating = r.Rating
        });

        return reviewDtos;
    }

}
