using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Cargodi.Business.Extensions;

public static class AuthExtension
{
	public static IServiceCollection AddIdentityService
		(this IServiceCollection services, IConfiguration configuration)
	{		
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{				

				options.TokenValidationParameters = new()
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),

					ValidateIssuer = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					
					ValidateAudience = true,
					ValidAudience = configuration["Jwt:Audience"],
					
					ValidateLifetime = true
				};
			});
			
		services.AddAuthorization(options => options.DefaultPolicy =
			new AuthorizationPolicyBuilder (JwtBearerDefaults.AuthenticationScheme)
				.RequireAuthenticatedUser()
				.Build());


		return services;
	}
}