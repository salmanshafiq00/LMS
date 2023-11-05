using Application.Common.Interfaces;

namespace Application.Features.Identity.Users.Queries;
public record GetApplicationUserByIdQuery(string UserId) : IRequest<ApplicationUserDto?>;

internal sealed class GetApplicationUserByIdQueryHandler : IRequestHandler<GetApplicationUserByIdQuery, ApplicationUserDto?>
{
    private readonly IIdentityService _identityService;

    public GetApplicationUserByIdQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<ApplicationUserDto?> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetUserAsync(request.UserId);
    }
}
