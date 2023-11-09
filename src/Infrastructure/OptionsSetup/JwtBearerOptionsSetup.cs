using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.OptionsSetup;
public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;
    private readonly IConfiguration _configuration;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions, IConfiguration configuration)
    {
        _jwtOptions = jwtOptions.Value;
        _configuration = configuration;
        var a = _configuration["JwtOptions:Issuer"];
    }
    public void Configure(JwtBearerOptions options)
    {
        options.IncludeErrorDetails = true;
        //options.TokenValidationParameters = new()
        //{
        //    ValidateIssuer = false,
        //    ValidateAudience = false,
        //    ValidateLifetime = true,
        //    ValidateIssuerSigningKey = true,
        //    ValidIssuer = _jwtOptions.Issuer,
        //    ValidAudience = _jwtOptions.Audience,
        //    IssuerSigningKey = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        //};

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["JwtOptions:Issuer"],
            ValidAudience = _configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]))
        };
    }
}
