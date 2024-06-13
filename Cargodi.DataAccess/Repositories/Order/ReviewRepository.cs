using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities.Order;
using Cargodi.DataAccess.Interfaces.Order;
using Microsoft.EntityFrameworkCore;

namespace Cargodi.DataAccess.Repositories.Order;

public class ReviewRepository : IReviewRepository
{
    private readonly DatabaseContext _databaseContext;
    
    public ReviewRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<Review> CreateOrderReviewAsync(Review review, CancellationToken cancellationToken)
    {
        var entry = await _databaseContext.Reviews.AddAsync(review, cancellationToken);

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public Task<List<Review>> GetOrderReviewsAsync(long orderId, CancellationToken cancellationToken)
    {
        return _databaseContext.Reviews.Where(r => r.OrderId == orderId).ToListAsync(cancellationToken);
    }
}