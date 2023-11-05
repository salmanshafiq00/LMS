using Application.Common.Interfaces;

namespace Application.Features.Identity.Roles.Queries;
public record GetIdentityRoleByIdQuery(string Id) : IRequest<IdentityRoleDto>;

internal sealed class GetIdentityRoleByIdQueryHandler : IRequestHandler<GetIdentityRoleByIdQuery, IdentityRoleDto>
{
    private readonly IIdentityRoleService _identityRoleService;

    public GetIdentityRoleByIdQueryHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }
    public async Task<IdentityRoleDto> Handle(GetIdentityRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _identityRoleService.GetRoleAsync(request.Id);
    }
}

