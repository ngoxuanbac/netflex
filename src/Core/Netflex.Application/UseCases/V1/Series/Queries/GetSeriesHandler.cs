using Netflex.Application.Interfaces.Repositories.ReadOnly;
using Netflex.Shared.Pagination;

namespace Netflex.Application.UseCases.V1.Series.Queries;

public record GetSeriesQuery(
    string? Search,
    long[]? KeywordIds,
    long[]? GenreIds,
    string? Country,
    int? Year,
    string? SortBy,
    string? FollowerId,
    int PageIndex,
    int PageSize
) : IQuery<PaginatedResult<SerieDto>>;

public class GetSeriesHandler(ISerieReadOnlyRepository serieReadOnlyRepository)
    : IQueryHandler<GetSeriesQuery, PaginatedResult<SerieDto>>
{
    private readonly ISerieReadOnlyRepository _serieReadOnlyRepository = serieReadOnlyRepository;
    public async Task<PaginatedResult<SerieDto>> Handle(GetSeriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _serieReadOnlyRepository.GetSeriesAsync(
            request.Search,
            request.KeywordIds,
            request.GenreIds,
            request.Country,
            request.Year,
            request.SortBy,
            request.FollowerId,
            request.PageIndex,
            request.PageSize,
            cancellationToken
        );
        return result;
    }
}