using Cargodi.DataAccess.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class ClientSeeds : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        var clients = new List<Client>()
        {
            new Client
            {
                Id = 1,
                UserId = 3,
                Name = "Афанасий"
            },
            new Client
            {
                Id = 2,
                UserId = 4,
                Name = "Валерий"
            },
            new Client
            {
                Id = 3,
                UserId = 5,
                Name = "Наталья"
            },
        };

        builder.HasData(clients);
    }

}