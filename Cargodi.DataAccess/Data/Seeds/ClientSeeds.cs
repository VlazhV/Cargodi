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
            }
        };

        builder.HasData(clients);
    }

}