using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SingleDataBase.Configuration;
using SingleDataBase.Features.Auth.Login;
using SingleDataBase.Features.Auth.Register;
using SingleDataBase.Features.Auth.UpdatePassword;

namespace SingleDataBase.Endpoints;

public static class AuthEndpoints
{

    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/login", async (
            LoginRequest request,
            IMediator mediator,
            IOptions<AuthConfiguration> jwtOptions) =>
        {
            var loginResult = await mediator.Send(request);

            return loginResult.Match(
                user => Results.Ok(new { Token = GenerateJwtToken(user, jwtOptions.Value) }),
                ex => Results.BadRequest(ex.Message)
            );
        });

        app.MapPost("api/auth/register", async (
            RegisterRequest request,
            IMediator mediator) =>
        {
            var registerResult = await mediator.Send(request);

            return registerResult.Match(
                id => Results.Ok(id),
                ex => Results.BadRequest(ex.Message)
            );
        });

        app.MapPost("api/auth/update-password", async (
            UpdatePasswordRequest request,
            IMediator mediator) =>
        {
            var updatePasswordResult = await mediator.Send(request);

            return updatePasswordResult.Match(
                _ => Results.Ok(),
                ex => Results.BadRequest(ex.Message)
            );
        });
    }

    private static string GenerateJwtToken(LoginView user, AuthConfiguration auth)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            issuer: auth.Issuer,
            audience: auth.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
