using Application.Common.Interfaces;

namespace Application.Features.Identity.Roles.Commands;
public record CreateIdentityRoleCommand (string RoleName) : IRequest<string>;

internal sealed class CreateIdentityRoleCommandHandler : IRequestHandler<CreateIdentityRoleCommand, string>
{
    private readonly IIdentityRoleService _identityRoleService;

    public CreateIdentityRoleCommandHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }
    public async Task<string> Handle(CreateIdentityRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _identityRoleService.CreateRoleAsync(command.RoleName);
        return result.RoleId;
    }
}
