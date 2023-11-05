using Application.Features.Identity.Roles.Queries;

namespace Application.Common.Interfaces;
public interface IIdentityRoleService
{
    Task<IReadOnlyCollection<IdentityRoleDto>> GetRolesAsync();
    Task<string?> GetRoleNameAsync(string roleId);
    Task<IdentityRoleDto?> GetRoleAsync(string roleId);
    Task<(Result Result, string RoleId)> CreateRoleAsync(string roleName);
    Task<(Result Result, string RoleId)> UpdateRoleAsync(string roleId, string roleName);
    Task<Result> DeleteRoleAsync(string roleId);
    Task<IList<string>> GetAllPermissionsAsync();
    Task<IList<string>> GetPermissionsByRoleAsync(string roleId);
    Task<(Result Result, string RoleId)> AssignOrRemovePermissionsToRoleAsync(string roleId, List<string> permissions);
}
