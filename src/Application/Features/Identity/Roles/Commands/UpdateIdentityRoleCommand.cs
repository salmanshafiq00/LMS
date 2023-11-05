using Application.Common.Interfaces;

namespace Application.Features.Identity.Roles.Commands;
public record UpdateIdentityRoleCommand (string RoleId, string RoleName) : IRequest<string>;

internal sealed class UpdateIdentityRoleCommandHandler : IRequestHandler<UpdateIdentityRoleCommand, string>
{
    private readonly IIdentityRoleService _identityRoleService;

    public UpdateIdentityRoleCommandHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }
    public async Task<string> Handle(UpdateIdentityRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _identityRoleService.UpdateRoleAsync(command.RoleId, command.RoleName);
        return result.RoleId;
    }
}
