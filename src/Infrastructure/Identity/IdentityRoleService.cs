using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Constants;
using Application.Features.Identity.Roles.Queries;
using Ardalis.GuardClauses;
using AutoMapper;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Identity;
internal class IdentityRoleService : IIdentityRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public IdentityRoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<IdentityRoleDto>> GetRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var mappedRoles = _mapper.Map<IReadOnlyCollection<IdentityRoleDto>>(roles);
        return mappedRoles;
    }

    public async Task<IdentityRoleDto?> GetRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        var mappedRole = _mapper.Map<IdentityRoleDto>(role);
        return mappedRole;
    }

    public async Task<string?> GetRoleNameAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        return role?.Name;
    }

    public async Task<(Result Result, string RoleId)> CreateRoleAsync(string roleName)
    {
        IdentityRole role = new() { Name = roleName, NormalizedName = roleName.ToUpper() };
        IdentityResult result = await _roleManager.CreateAsync(role);
        return (result.ToApplicationResult(), role.Id);
    }

    public async Task<(Result Result, string RoleId)> UpdateRoleAsync(string roleId, string roleName)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            Guard.Against.NotFound(roleId, roleName);

        role.Name = roleName;
        role.NormalizedName = roleName.ToUpper();

        var result = await _roleManager.UpdateAsync(role);

        return (result.ToApplicationResult(), role.Id);
    }

    public async Task<Result> DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        return role != null ? await DeleteRoleAsync(role) : Result.Success();
    }

    public async Task<IList<string>> GetAllPermissionsAsync()
    {
        var permissions = new List<string>();
        // Get All Nested Module / Class Type
        var moduleTypeList = Permissions.GetAllNestedModuleType();

        // Get All Permission belongs to a nested module/class
        foreach (var module in moduleTypeList)
        {
            permissions.GetAllConstantPermissions(module);
        }

        return await Task.FromResult(permissions);
    }

    public async Task<IList<string>> GetPermissionsByRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            Guard.Against.NotFound(roleId, roleId);

        var rolePermissions = await _roleManager.GetClaimsAsync(role);

        return rolePermissions.Select(p => p.Value.ToString()).ToList();
    }

    public async Task<(Result Result, string RoleId)> AssignOrRemovePermissionsToRoleAsync(string roleId, List<string> permissions)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role == null)
            Guard.Against.NotFound(roleId, roleId);

        var existingPermissions = await _roleManager.GetClaimsAsync(role);

        IdentityResult result = new();

        // Remove All permissions
        foreach (var permission in existingPermissions)
        {
            result = await _roleManager.RemoveClaimAsync(role, permission);
        }

        // Add All selected permissions
        foreach (var permission in permissions)
        {
            result = await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
        }

        return (result.ToApplicationResult(), roleId);
    }

    private async Task<Result> DeleteRoleAsync(IdentityRole role)
    {
        var result = await _roleManager.DeleteAsync(role);
        return result.ToApplicationResult();
    }

}
