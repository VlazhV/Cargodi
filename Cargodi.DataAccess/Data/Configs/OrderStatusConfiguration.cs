using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Configs;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasData(
            OrderStatuses.Processing,
            OrderStatuses.Accepted,
            OrderStatuses.Declined,
            OrderStatuses.Completing
        );
    }
}