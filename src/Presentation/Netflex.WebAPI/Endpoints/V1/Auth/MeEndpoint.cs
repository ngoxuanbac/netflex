using System.Security.Claims;
using Netflex.Application.Common.Exceptions;
using Netflex.Application.UseCases.V1.Users.Queries;

namespace Netflex.WebAPI.Endpoints.V1.Auth;

public class MeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/me", async (ISender sender, HttpContext context) =>
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new NotAuthenticatedException();

            var result = await sender.Send(new GetUserDetailQuery(userId));
            return Results.Ok(result.User);
        })
        .RequireAuthorization()
        .MapToApiVersion(1)
        .WithName(nameof(MeEndpoint));
    }
}