using Application.Features.Identity.Roles.Queries;
using AutoMapper;
using System.Security.Claims;

namespace Infrastructure.Mapping;
internal class ClaimProfile : Profile
{
    public ClaimProfile()
    {
        CreateMap<Claim, PermissionDto>();
    }
}
