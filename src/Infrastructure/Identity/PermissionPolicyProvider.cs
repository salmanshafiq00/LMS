using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity;
public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if(policy is null)
        {
            var permissions = policyName;

            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermisssionRequirement(permissions))
                .Build();
        }

        return policy;
    }
}
