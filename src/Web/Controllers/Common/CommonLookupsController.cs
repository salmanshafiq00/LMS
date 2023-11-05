using Application.Features.CommonLookups.Commands;
using Application.Features.CommonLookups.Quries;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers.Common;
[Route("api/[controller]")]
[ApiController]
public class CommonLookupsController : BaseApiController
{
    private readonly IHttpContextAccessor _httpContext;

    public CommonLookupsController(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    [HttpGet]
    public async Task<IResult> Get()
    {
        // test purpose
        string? username = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        string? userId = _httpContext.HttpContext?.User?.FindFirstValue("uid");
        var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        return Results.Ok(await Mediator.Send(new GetCommonLookupsQuery()));
    }

    [HttpPost]
    public async Task<IResult> Create(CreateCommonLookupCommand command)
    {
        return Results.Ok(await Mediator.Send(command));
    }
}
