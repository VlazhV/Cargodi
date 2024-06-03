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
                Length = Generator.GenerateRandomNumber(5, 20),
                Width = Generator.GenerateRandomNumber(5, 20),
                Height = Generator.GenerateRandomNumber(5, 20),
                Weight = Generator.GenerateRandomNumber(5, 20),
                Description = "",
                OrderId = Generator.GenerateRandomNumber(1, 5),
                PayloadTypeId = 1
            });
        }

        builder.HasData(payloads);
    }

}