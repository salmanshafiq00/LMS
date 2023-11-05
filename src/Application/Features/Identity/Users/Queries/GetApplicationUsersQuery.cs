using Application.Common.Interfaces;

namespace Application.Features.Identity.Users.Queries;
public record GetApplicationUsersQuery : IRequest<IList<ApplicationUserDto>>;

internal sealed class GetApplicationUsersQueryHandler : IRequestHandler<GetApplicationUsersQuery, IList<ApplicationUserDto>>
{
    private readonly IIdentityService _identityService;

    public GetApplicationUsersQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<IList<ApplicationUserDto>> Handle(GetApplicationUsersQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetUsersAsync();
    }
}