using Cargodi.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class UserSeeds : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var users = new List<User>()
        {
            new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@",
                NormalizedEmail = "ADMIN@MAIL.RU",
                PasswordHash = "AQAAAAIAAYagAAAAEPY7WVyiuYib5RDDYNF3XssipR4zfeSYWmJCBMUK0QRuLHmIlcIYuWqdt5eYK3kF3A==",
                ConcurrencyStamp = "1ce8f54c-8405-4d82-a3aa-797ab2b45550",
                PhoneNumber = "+375441114488"
            },
            
            new User
            {
                Id = 2,
                UserName = "operator1",
                NormalizedUserName = "OPERATOR1",
                Email = "operator1@mail.ru",
                NormalizedEmail = "OPERATOR1@MAIL.RU",
                PasswordHash = "AQAAAAIAAYagAAAAEH/G2ct0XwgOoXaRTJgWkU46UKxDqXUiRQvfn4x00qh/zkq5V1XNx4ChedR0p63gDA==",
                ConcurrencyStamp = "2586ec42-77b4-47f3-b8f4-7b636dd678d3",
                PhoneNumber = "+375445114488"
            },
            
            new User
            {
                Id = 3,
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "client1@mail.ru",
                NormalizedEmail = "CLIENT1@MAIL.RU",
                PasswordHash = "AQAAAAIAAYagAAAAELYjdYNv1Bu8vDEoo2K88b9JlZ5nnKaj0mWHfnpsLNxVbhgr2sI38TT+OeybjlQQTQ==",
                ConcurrencyStamp = "bb3c7e16-3044-432e-a2f0-3fb528cfd048",
                PhoneNumber = "+375442114488"
            },
            new User
            {
                Id = 4,
                UserName = "client2",
                NormalizedUserName = "CLIENT2",
                Email = "client2@mail.ru",
                NormalizedEmail = "CLIENT3@MAIL.RU",
                PasswordHash = "AQAAAAIAAYagAAAAECnMN9e0UKaIefkZrStZ+cLcg3tnXmByAVeP1EAFT5/klg1w5sbWtIBftabugKpg5Q==",
                ConcurrencyStamp = "0747c06e-9ea8-4716-8c1c-facd90a2684b",
                PhoneNumber = "+375443114488"
            },
            
            new User
            {
                Id = 5,
                UserName = "driver1",
                NormalizedUserName = "DRIVER1",
                Email = "driver1@mail.ru",
                NormalizedEmail = "DRIVER1@MAIL.RU",
                PasswordHash = "AQAAAAIAAYagAAAAEH4egC8xBaRAzXjWavUWB3vBXn4asC7mzChJRWRYUSZIu8hg0DWadayXzCGY/+crjg==",
                ConcurrencyStamp = "9dc1d642-115f-4eb6-8c68-059c55b715c1",
                PhoneNumber = "+375447114488"
            },
            new User
            {
                Id = 6,
                UserName = "driver2",
                NormalizedUserName = "DRIVER2",
                Email = "driver2@mail.ru",
                NormalizedEmail = "DRIVER2@MAIL.RU",
                PasswordHash = "AQAAAAIAAYagAAAAEDb0E7W3P+NIVtSt3hKKYig8Qnu3IXDQFxlnjCE+OHyeP0vhjLNimpFMxY9rDJoixQ==",
                ConcurrencyStamp = "c2f7b8e0-6e77-44b9-8761-7f4a4167ceaa",
                PhoneNumber = "+375448114488"
            },
        };

        builder.HasData(users);
    }

}