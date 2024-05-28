using Cargodi.Business.DTOs.Identity;

namespace Cargodi.Business.DTOs.Staff.Client;

public class GetClientUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public UserIdDto User { get; set; } = null!;
}
