using Application.Features.CommonLookups.Commands;
using Application.Features.CommonLookups.Quries;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers.Common;
[Route("api/[controller]")]
[ApiController]
public class CommonLookupsController : BaseApiController
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommonLookupsController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    [HttpGet]
    public async Task<IResult> Get()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        return Results.Ok(await Mediator.Send(new GetCommonLookupsQuery()));
    }

    [HttpPost]
    public async Task<IResult> Create(CreateCommonLookupCommand command)
    {
        return Results.Ok(await Mediator.Send(command));
    }
}
