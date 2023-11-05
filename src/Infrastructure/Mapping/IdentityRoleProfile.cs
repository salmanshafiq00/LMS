using Application.Features.Identity.Roles.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Mapping;
internal class IdentityRoleProfile : Profile
{
    public IdentityRoleProfile()
    {
        CreateMap<IdentityRole, IdentityRoleDto>();
    }
}
