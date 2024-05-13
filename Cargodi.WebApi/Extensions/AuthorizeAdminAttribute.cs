using Microsoft.AspNetCore.Authorization;

namespace Cargodi.WebApi.Extensions;

public class AuthorizeAdminAttribute: AuthorizeAttribute
{
    public AuthorizeAdminAttribute(): base()
	{
		Roles = DataAccess.Constants.Roles.Admin;
	}
	
	public AuthorizeAdminAttribute(string policy): base(policy)
	{
		Roles = DataAccess.Constants.Roles.Admin;
	}
}