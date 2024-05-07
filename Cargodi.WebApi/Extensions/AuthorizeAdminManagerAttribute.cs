using Microsoft.AspNetCore.Authorization;

namespace Cargodi.WebApi.Extensions;

public class AuthorizeAdminManagerAttribute: AuthorizeAttribute
{
    public AuthorizeAdminManagerAttribute(): base()
	{
		Roles = string.Concat(DataAccess.Constants.Roles.Admin, ", ", DataAccess.Constants.Roles.Manager);
	}
	
	public AuthorizeAdminManagerAttribute(string policy): base(policy)
	{
		Roles = string.Concat(DataAccess.Constants.Roles.Admin, ", ", DataAccess.Constants.Roles.Manager);
	}
}