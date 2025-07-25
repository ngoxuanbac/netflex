using Netflex.Application.UseCases.V1.Movies.Queries;
using Netflex.Shared.Pagination;

namespace Netflex.WebAPI.Endpoints.V1.Movies;

public record GetMoviesRequest(
    string? Search,
    string? Genres,
    string? Keywords,
    string? Actors,
    string? Country,
    int? Year,
    string? SortBy,
    string? FollowerId,
    int PageIndex = 1,
    int PageSize = 10
) : PaginationRequest(PageIndex, PageSize);

public class GetMoviesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/movies", async ([AsParameters] GetMoviesRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetMoviesQuery>();
            query = query with
            {
                GenreIds = request.Genres?.Split(',')
                  .Select(x => long.TryParse(x.Trim(), out var id) ? id : 0)
                  .Where(id => id != 0).ToArray() ?? [],

                KeywordIds = request.Keywords?.Split(',')
                  .Select(x => long.TryParse(x.Trim(), out var id) ? id : 0)
                  .Where(id => id != 0).ToArray() ?? [],

                ActorIds = request.Actors?.Split(',')
                 .Select(x => long.TryParse(x.Trim(), out var id) ? id : 0)
                 .Where(id => id != 0).ToArray() ?? [],
            };
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithName(nameof(GetMoviesEndpoint));
    }
}