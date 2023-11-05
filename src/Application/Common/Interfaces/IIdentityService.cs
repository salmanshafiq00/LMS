using Application.Features.Identity.Users.Queries;

namespace Application.Common.Interfaces;
public interface IIdentityService
{
    Task<IList<ApplicationUserDto>> GetUsersAsync();
    Task<ApplicationUserDto?> GetUserAsync(string userId);
    Task<string> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
    Task<(Result Result, string UserId)> UpdateUserAsync(string id, string userName);
    Task<Result> DeleteUserAsync(string userId);
}
