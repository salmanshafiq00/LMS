namespace Application.Features.Identity.Roles.Queries;
public record IdentityRoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    public List<PermissionDto> Permissions { get; set; }
}
