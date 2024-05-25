using Cargodi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class AddressSeeds : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        var data = new List<Address>
        {
            new Address
            {
                Id = 1,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, ул. Светлая, 23",
                Latitude = 53.935541d,
                Longitude = 27.626106d,
            },
            new Address
            {
                Id = 2,
                IsNorth = true,
                IsWest = false,
                Name = "Минск р-н, ул. Родниковая, 2",
                Latitude = 53.945487d,
                Longitude = 27.094536d,

            },
            new Address
            {
                Id = 3,
                IsNorth = true,
                IsWest = false,
                Name = "Минск р-н, Сеница, Слуцкая улица, 37А",
                Latitude = 53.825227d,
                Longitude = 27.536000d,
            },
            new Address
            {
                Id = 4,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, ул. Народная, 29",
                Latitude = 53.873240d,
                Longitude = 27.625467d,
            }
        };

        builder.HasData(data);
    }

}