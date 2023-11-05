using CleanArchitecture.Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Identity;
internal sealed class JwtProvider : IJwtProvider
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOptions _jwtOptions;

    public JwtProvider( 
        IOptions<JwtOptions> jwtOptions,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtOptions = jwtOptions.Value;
    }
    public async Task<string> GenerateJwtAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var userRoles = await _userManager.GetRolesAsync(user!);

        var roleClaims = new List<Claim>();
        foreach (var role in userRoles)
        {
            var identityRole = await _roleManager.FindByNameAsync(role);
            roleClaims.AddRange(await _roleManager.GetClaimsAsync(identityRole!));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user!.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("username", user.UserName!),
            new Claim("uid", user.Id!),
            new Claim("ip", GetIpAddress())
        }
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
             _jwtOptions.Issuer,
             _jwtOptions.Audience,
             claims,
             null,
             DateTime.Now.AddMinutes(_jwtOptions.DurationInMinutes),
             signingCredentials
            );
        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return await Task.FromResult(tokenValue);
    }

    private static string GetIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return string.Empty;
    }
}
