using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class RoleSeeds : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        var userRoles = new List<IdentityUserRole<long>>
        {
            new IdentityUserRole<long>
            {
                UserId = 1,
                RoleId = 3,
            },

            new IdentityUserRole<long>
            {
                UserId = 2,
                RoleId = 2,
            },

            new IdentityUserRole<long>
            {
                UserId = 3,
                RoleId = 1,
            },
            new IdentityUserRole<long>
            {
                UserId = 4,
                RoleId = 1,
            },

            new IdentityUserRole<long>
            {
                UserId = 5,
                RoleId = 4,
            },
            new IdentityUserRole<long>
            {
                UserId = 6,
                RoleId = 4,
            },
        };

        builder.HasData(userRoles);
    }

}