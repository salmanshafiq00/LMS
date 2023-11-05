using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Constants;
using Ardalis.GuardClauses;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;
internal class AuthAccountService : IAuthAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtProvider _jwtProvider;

    public AuthAccountService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }
    public Task<(Result Result, string UserId)> ForgotPassword(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<(Result Result, string Token)> Login(string username, string password)
    {
        var user = await _userManager.FindByEmailAsync(username)
            ?? await _userManager.FindByNameAsync(username);

        // new NotFoundException(username, nameof(ApplicationUser));
        Guard.Against.NotFound(username, CommonMessage.WRONG_USERNAME_PASSWORD);

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
        {
            return (Result.Failure(new List<string> { CommonMessage.WRONG_USERNAME_PASSWORD }), "");
        }

        var token = await _jwtProvider.GenerateJwtAsync(user.Id);

        return !string.IsNullOrEmpty(token)
            ? (Result.Success(), token)
            : (Result.Failure(new List<string> { CommonMessage.WRONG_USERNAME_PASSWORD }), token);
    }

    public Task<(Result Result, string UserId)> ResetPassword(string email)
    {
        throw new NotImplementedException();
    }
}
