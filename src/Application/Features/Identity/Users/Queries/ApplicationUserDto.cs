namespace Application.Features.Identity.Users.Queries;
public record ApplicationUserDto
{
    public string Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Password { get; set; }
}
