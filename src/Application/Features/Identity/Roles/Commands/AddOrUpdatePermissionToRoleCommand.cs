using Application.Common.Interfaces;
using Application.Features.Identity.Roles.Queries;

namespace Application.Features.Identity.Roles.Commands;
public record AddOrUpdatePermissionToRoleCommand(string RoleId,  List<PermissionDto> Permissions) : IRequest<int>;

internal sealed class AddOrUpdatePermissionToRoleCommandHandler : IRequestHandler<AddOrUpdatePermissionToRoleCommand, int>
{
    private readonly IIdentityRoleService _identityRoleService;

    public AddOrUpdatePermissionToRoleCommandHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }
    public Task<int> Handle(AddOrUpdatePermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
