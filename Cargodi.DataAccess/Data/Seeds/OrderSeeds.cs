using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class OrderSeeds : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        var orders = new List<Order>();
        
        
        for (int i = 1; i <= 22; i++)
        {
            var time = new DateTime(
                2024,
                Generator.GenerateRandomNumber(1, 4),
                Generator.GenerateRandomNumber(1, 28),
                Generator.GenerateRandomNumber(5, 20),
                Generator.GenerateRandomNumber(0, 59),
                Generator.GenerateRandomNumber(0, 59)
            );

            DateTime? accessTime = Generator.GenerateRandomNumber(0, 99) < 85 ? DateTime.UtcNow : null;

            int? operatorId = accessTime.HasValue ?
                Generator.GenerateRandomNumber(1, 2) :
                null;

            var status = accessTime == null ? OrderStatuses.Processing : OrderStatuses.Accepted;

            orders.Add(new Order
            {
                Id = i,
                Time = time,
                AcceptTime = accessTime,
                LoadAddressId = i + 2,
                DeliverAddressId = i + 2 + 5,
                ClientId = Generator.GenerateRandomNumber(1, 2),
                OrderStatusId = status.Id,
                OperatorId = operatorId
            });
        }

        builder.HasData(orders);
    }

}