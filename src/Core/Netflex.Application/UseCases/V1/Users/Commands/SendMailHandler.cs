using Netflex.Domain.ValueObjects;

namespace Netflex.Application.UseCases.V1.Users.Commands;

public class SendMailCommand : ICommand
{
    public string To { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
}

public class SendMailHandler(IUnitOfWork unitOfWork, IEmailService emailService)
    : ICommandHandler<SendMailCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailService _emailService = emailService;

    public async Task<Unit> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        var hasExists = await _unitOfWork.Repository<Domain.Entities.User>()
            .ExistsAsync(u => u.Email == Email.Of(request.To), cancellationToken);
        if (!hasExists) throw new UserNotFoundException();

        await _emailService.SendEmailAsync(request.To, request.Subject, request.Body, cancellationToken);
        return Unit.Value;
    }
}