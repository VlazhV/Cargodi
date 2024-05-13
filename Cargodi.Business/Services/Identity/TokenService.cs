using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cargodi.Business.Services.Identity;

public class TokenService : ITokenService
{
   private readonly IConfiguration _configuration;
	private readonly IUserRepository _userRepository;
	public TokenService(IConfiguration configuration, IUserRepository userRepository)
	{
		_configuration = configuration;
		_userRepository = userRepository;
	}
	
	private async Task<List<Claim>> GetClaimsAsync(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
		};

		var role = (await _userRepository.GetRolesAsync(user)).First();
		claims.Add(new Claim(ClaimTypes.Role, role));

		return claims;
	}

	private SecurityTokenDescriptor GetTokenDescriptor(List<Claim> claims, SigningCredentials creds)
	{
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddDays(1),
			SigningCredentials = creds,
			Audience = _configuration["Jwt:Audience"],
			Issuer = _configuration["Jwt:Issuer"],
		};

		return tokenDescriptor;
	}

	public async Task<string> GetTokenAsync(User user)
	{
		var claims = await GetClaimsAsync(user);
		var key = new SymmetricSecurityKey(
			Encoding.Default.GetBytes(_configuration["Jwt:Secret"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
		var tokenDescriptor = GetTokenDescriptor(claims, creds);
		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}

}