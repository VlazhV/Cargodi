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
            },
            
            new Address
            {
                Id = 5,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, ул. Москвина, 1",
                Latitude = 53.913357d,
                Longitude = 27.525996d,
            },
            new Address
            {
                Id = 6,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, улица Стефании Станюты, 17",
                Latitude = 53.950822d,
                Longitude = 27.569049d,

            },
            new Address
            {
                Id = 7,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, площадь Свободы, 11",
                Latitude = 53.903579d,
                Longitude = 27.554374d,
            },
            new Address
            {
                Id = 8,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, проспект Машерова, 35А",
                Latitude = 53.916353d,
                Longitude = 27.549897d,
            },
            
            
             new Address
            {
                Id = 9,
                IsNorth = true,
                IsWest = false,
                Name = "р-н Минск, Якубовичи, Луговая улица, 26",
                Latitude = 53.976832d,
                Longitude = 27.544625d,
            },
            
            new Address
            {
                Id = 10,
                IsNorth = true,
                IsWest = false,
                Name = "Любимая улица, 2, деревня Дроздово, Боровлянский сельсовет, Минский район",
                Latitude = 53.990738d,
                Longitude = 27.627642d,
            },
            new Address
            {
                Id = 11,
                IsNorth = true,
                IsWest = false,
                Name = "Минская улица, 7, агрогородок Колодищи, Минский район",
                Latitude = 53.955251d,
                Longitude = 27.776284d,

            },
            new Address
            {
                Id = 12,
                IsNorth = true,
                IsWest = false,
                Name = "Парковая улица, 65, агрогородок Чуриловичи, Михановичский сельсовет, Минский район",
                Latitude = 53.746404d,
                Longitude = 27.566379d,
            }
        };

        builder.HasData(data);
    }

}