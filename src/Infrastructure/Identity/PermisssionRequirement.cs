using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Identity;

// this tell what is the permission that we are handling for the authorization request
public class PermisssionRequirement : IAuthorizationRequirement
{
    public PermisssionRequirement(string permission)
    {
        Permission = permission;
    }
    public string Permission { get; }

}
