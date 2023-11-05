using Application.Common.Interfaces;

namespace Application.Features.Identity.Roles.Queries;
public record GetPermissionsByRoleIdQuery(string RoleId) : IRequest<List<PermissionDto>>;

internal sealed class GetPermissionsByRoleIdQueryHandler : IRequestHandler<GetPermissionsByRoleIdQuery, List<PermissionDto>>
{
    private readonly IIdentityRoleService _identityRoleService;

    public GetPermissionsByRoleIdQueryHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }
    public Task<List<PermissionDto>> Handle(GetPermissionsByRoleIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
