using Microsoft.AspNetCore.Authorization;

namespace Cargodi.WebApi.Extensions;

public class AuthorizeAdminManagerDriverAttribute: AuthorizeAttribute
{
    public AuthorizeAdminManagerDriverAttribute(): base()
	{
		Roles = string.Concat(DataAccess.Constants.Roles.Admin, ", ", 
			DataAccess.Constants.Roles.Manager, ", ",
			DataAccess.Constants.Roles.Driver);
	}
	
	public AuthorizeAdminManagerDriverAttribute(string policy): base(policy)
	{
		Roles = string.Concat(DataAccess.Constants.Roles.Admin, ", ", 
			DataAccess.Constants.Roles.Manager, ", ",
			DataAccess.Constants.Roles.Driver);
	}
}