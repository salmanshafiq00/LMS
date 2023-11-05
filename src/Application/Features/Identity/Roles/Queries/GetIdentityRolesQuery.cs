using Application.Common.Interfaces;

namespace Application.Features.Identity.Roles.Queries;
public record GetIdentityRolesQuery : IRequest<List<IdentityRoleDto>>;

internal sealed class GetIdentityRolesQueryHandler : IRequestHandler<GetIdentityRolesQuery, IReadOnlyCollection<IdentityRoleDto>>
{
    private readonly IIdentityRoleService _identityRoleService;

    public GetIdentityRolesQueryHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }
    public async Task<IReadOnlyCollection<IdentityRoleDto>> Handle(GetIdentityRolesQuery request, CancellationToken cancellationToken)
    {
        return await _identityRoleService.GetRolesAsync();
    }
}

