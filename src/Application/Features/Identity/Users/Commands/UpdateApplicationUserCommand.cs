using Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Identity.Users.Commands;
public record UpdateApplicationUserCommand(string Id, [EmailAddress] string Email) : IRequest<string>
{
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

internal sealed class UpdateApplicationUserQueryHandler : IRequestHandler<UpdateApplicationUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public UpdateApplicationUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(UpdateApplicationUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _identityService.UpdateUserAsync(command.Id, command.Email);
        return result.UserId;
    }
}
