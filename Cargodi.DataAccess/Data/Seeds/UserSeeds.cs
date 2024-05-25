using Cargodi.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargodi.DataAccess.Data.Seeds;

public class UserSeeds : IEntityTypeConfiguration<User>
{
    private readonly UserManager<User> _userManager;
    
    public UserSeeds(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var users = new List<User>()
        {
            new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@"
            }
        };
    }

}