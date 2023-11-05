namespace CleanArchitecture.Application.Common.Interfaces;
public interface IJwtProvider
{
    Task<string> GenerateJwtAsync(string userId);
}
