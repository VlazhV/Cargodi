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
                Name = "Минский район, Якубовичи, Луговая улица, 26",
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
            },
            //-----
            new Address
            {
                Id = 13,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, ул. Притыцкого, д. 90",
                Latitude = 53.909333,
                Longitude =  27.445679,
            },
            new Address
            {
                Id = 14,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, улица Чичерина, 21",
                Latitude = 53.915491, 
                Longitude = 27.565981,
            },new Address
            {
                Id = 15,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, ул. К. Маркса, 42",
                Latitude = 53.746404d,
                Longitude = 27.566379d,
            },new Address
            {
                Id = 16,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, ул. Бехтерева, 12",
                Latitude = 53.901902, 
                Longitude = 27.566655,
            },new Address
            {
                Id = 17,
                IsNorth = true,
                IsWest = false,
                Name = "г. Минск, ул. Казинца, 6а",
                Latitude = 53.866110, 
                Longitude = 27.521057,
            },new Address
            {
                Id = 18,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, ул. Некрасова, 110",
                Latitude = 53.939873, 
                Longitude = 27.572871
            },new Address
            {
                Id = 19,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, ул. Гусовского, 2",
                Latitude = 53.901302, 
                Longitude = 27.516251
            },new Address
            {
                Id = 20,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, Орловская ул., 40",
                Latitude = 53.932734, 
                Longitude = 27.555669,
            },
            new Address
            {
                Id = 21,
                IsNorth = true,
                IsWest = false,
                Name = "агрогородок Колодищи, Минская ул., 56",
                Latitude = 53.955442, 
                Longitude = 27.763045
            },
            new Address
            {
                Id = 22,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, ул. Бирюзова, 4, корп. 1",
                Latitude = 53.916382,
                Longitude = 27.512361
            },
            new Address
            {
                Id = 23,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, Центральная ул., 3А",
                Latitude = 53.864798, 
                Longitude = 27.650450
            },
            new Address
            {
                Id = 24,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, Промышленный пер., 9",
                Latitude = 53.837896, 
                Longitude = 27.678666
            },
            
            //-----------
            
            new Address
            {
                Id = 25,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, ул. Франциска Скорины, 54А",
                Latitude = 53.934748,
                Longitude = 27.706145
            },
            new Address
            {
                Id = 26,
                IsNorth = true,
                IsWest = false,
                Name = "Минский район, Боровлянский сельсовет, деревня Боровая",
                Latitude = 53.964936,
                Longitude = 27.644485
            },
            new Address
            {
                Id = 27,
                IsNorth = true,
                IsWest = false,
                Name = "улица Фабрициуса, 8",
                Latitude = 53.890185,
                Longitude = 27.537094
            },
            new Address
            {
                Id = 28,
                IsNorth = true,
                IsWest = false,
                Name = "Минск, ул. Жилуновича, 9/2",
                Latitude = 53.880724, 
                Longitude = 27.639383
            },
            new Address
            {
                Id = 29,
                IsNorth = true,
                IsWest = false,
                Name = "Кольцевая улица, 20, Цнянка, Папернянский сельсовет, Минский район",
                Latitude = 53.971411,
                Longitude = 27.538847
            },
            
        };

        builder.HasData(data);
    }
}