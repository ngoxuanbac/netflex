using Netflex.Domain.Events;

namespace Netflex.Application.UseCases.V1.TVSerie.EventHandlers;

public class TVSerieCreatedEventHandler(ILogger<TVSerieCreatedEventHandler> logger, ISender sender)
        : INotificationHandler<TVSerieCreatedEvent>
{
    private readonly ILogger<TVSerieCreatedEventHandler> _logger = logger;
    private readonly ISender _sender = sender;

    public async Task Handle(TVSerieCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event handled: {DomainEvent}", nameof(TVSerieCreatedEvent));
        await Task.CompletedTask;
    }
}