using Cargodi.DataAccess.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class PayloadSeeds : IEntityTypeConfiguration<Payload>
{
    public void Configure(EntityTypeBuilder<Payload> builder)
    {
        var payloads = new List<Payload>();
        
        for (int i = 1; i <= 8; i++)
        {
            payloads.Add(new Payload
            {
                Id = i,
                Length = Generator.GenerateRandomNumber(500, 7_000),
                Width = Generator.GenerateRandomNumber(500, 7_000),
                Height = Generator.GenerateRandomNumber(500, 7_000),
                Weight = Generator.GenerateRandomNumber(500, 5_000_000),
                Description = "",
                OrderId = Generator.GenerateRandomNumber(1, 5),
                PayloadTypeId = Generator.GenerateRandomNumber(1, 3)
            });
        }

        builder.HasData(payloads);
    }

}