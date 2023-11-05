using Application.Constants;
using Domain.Constants;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using static Application.Constants.Permissions;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

internal class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()

    {
        // Default Roles
        var superadminRole = new IdentityRole(Roles.SuperAdmin.ToLower());

        if (_roleManager.Roles.All(r => r.Name != superadminRole.Name))
        {
            await _roleManager.CreateAsync(superadminRole);
        }

        // Default Users
        var superadmin = new ApplicationUser { UserName = "superadmin", Email = "superadmin@localhost" };

        if (_userManager.Users.All(u => u.UserName != superadmin.UserName || u.Email != superadmin.Email))
        {
            await _userManager.CreateAsync(superadmin, "123Password!");

            if (!string.IsNullOrWhiteSpace(superadminRole.Name))
            {
                await _userManager.AddToRolesAsync(superadmin, new[] { superadminRole.Name });
            }
        }

        // Default permissions
        await _roleManager.AddClaimAsync(superadminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.View));
        await _roleManager.AddClaimAsync(superadminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.Edit));
        await _roleManager.AddClaimAsync(superadminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.Create));
        await _roleManager.AddClaimAsync(superadminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.Delete));
    }
}
