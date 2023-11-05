using Application.Features.Identity.Accounts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Account;

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseApiController
{
    [HttpPost]
    public async Task<IResult> Login(LoginRequestCommand loginRequest)
    {
        return Results.Ok(await Mediator.Send(loginRequest));
    }
}
