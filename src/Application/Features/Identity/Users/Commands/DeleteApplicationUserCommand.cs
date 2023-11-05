using Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Identity.Users.Commands;
public record DeleteApplicationUserCommand(string Id, [EmailAddress] string Email) : IRequest<string>
{
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

internal sealed class DeleteApplicationUserQueryHandler : IRequestHandler<DeleteApplicationUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public DeleteApplicationUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(DeleteApplicationUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _identityService.UpdateUserAsync(command.Id, command.Email);
        return result.UserId;
    }
}
