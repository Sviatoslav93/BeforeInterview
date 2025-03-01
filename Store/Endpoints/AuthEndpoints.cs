using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Result.Extensions;
using Store.Configuration;
using Store.Features.Auth.Login;
using Store.Features.Auth.Register;
using Store.Features.Auth.UpdatePassword;

namespace Store.Endpoints;

public static class AuthEndpoints
{

    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        // POST api/auth/login
        app.MapPost("api/auth/login", (
            LoginRequest request,
            IMediator mediator,
            IOptions<AuthConfiguration> jwtOptions) =>
                mediator.Send(request)
                    .MatchAsync(
                        user => Results.Ok(new { Token = GenerateJwtToken(user, jwtOptions.Value) }),
                        err => Results.BadRequest(err.First().Message)
                    ));

        // POST api/auth/register
        app.MapPost("api/auth/register", (
            RegisterRequest request,
            IMediator mediator) =>
                mediator.Send(request)
                    .MatchAsync(
                        id => Results.Ok(id),
                        err => Results.BadRequest(err.First().Message)
                    ));

        // POST api/auth/update-password
        app.MapPost("api/auth/update-password", (
            UpdatePasswordRequest request,
            IMediator mediator) =>
                mediator.Send(request)
                    .MatchAsync(
                        _ => Results.Ok(),
                        err => Results.BadRequest(err.First().Message)
                    ));
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
