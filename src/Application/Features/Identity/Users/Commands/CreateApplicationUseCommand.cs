using Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Application.Features.Identity.Users.Commands;
public record CreateApplicationUseCommand([EmailAddress] string Email, string Password) : IRequest<string>
{
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

internal sealed class CreateApplicationUserQueryHandler : IRequestHandler<CreateApplicationUseCommand, string>
{
    private readonly IIdentityService _identityService;

    public CreateApplicationUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateApplicationUseCommand command, CancellationToken cancellationToken)
    {
        var result = await _identityService.CreateUserAsync(command.Email, command.Password);
        return result.UserId;
    }
}
