using Cargodi.DataAccess.Entities;

namespace Cargodi.Business.Interfaces.Identity;

public interface ITokenService
{
    Task<string> GetTokenAsync(User user);
}