using Cargodi.DataAccess.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class PayloadSeeds : IEntityTypeConfiguration<Payload>
{
    public void Configure(EntityTypeBuilder<Payload> builder)
    {
        var payloads = new List<Payload>();
        
        for (int i = 1; i <= 32; i++)
        {
            var orderId = i / 3 + 1;
            payloads.Add(new Payload
            {
                Id = i,
                Length = Generator.GenerateRandomNumber(5, 20),
                Width = Generator.GenerateRandomNumber(5, 20),
                Height = Generator.GenerateRandomNumber(5, 20),
                Weight = Generator.GenerateRandomNumber(5, 20),
                Description = "",
                OrderId = orderId,
                PayloadTypeId = 1
            });
        }
        
        for (int i = 33; i <= 38; i++)
        {
            var orderId = i - 22;
            payloads.Add(new Payload
            {
                Id = i,
                Length = Generator.GenerateRandomNumber(5, 20),
                Width = Generator.GenerateRandomNumber(5, 20),
                Height = Generator.GenerateRandomNumber(5, 20),
                Weight = Generator.GenerateRandomNumber(5, 20),
                Description = "",
                OrderId = orderId,
                PayloadTypeId = 2
            });
        }
        
        for (int i = 39; i <= 44; i++)
        {
            var orderId = i - 22;
            payloads.Add(new Payload
            {
                Id = i,
                Length = Generator.GenerateRandomNumber(5, 20),
                Width = Generator.GenerateRandomNumber(5, 20),
                Height = Generator.GenerateRandomNumber(5, 20),
                Weight = Generator.GenerateRandomNumber(5, 20),
                Description = "",
                OrderId = orderId,
                PayloadTypeId = 3
            });
        }

        builder.HasData(payloads);
    }

}