using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.QueueTickets.Commands;

public class IssueTicketCommand : IRequest<string> { }

public class IssueTicketCommandHandler : IRequestHandler<IssueTicketCommand, string>
{
    private readonly IUnitOfWork _uow;
    public IssueTicketCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<string> Handle(IssueTicketCommand request, CancellationToken ct)
    {
        var active = await _uow.QueueTickets.FindAsync(q => q.IsActive, ct);
        var latest = active.OrderByDescending(q => q.Id).FirstOrDefault();
        string next = GetNextQueue(latest?.TicketNumber ?? "00");

        var ticket = new QueueTicket { TicketNumber = next, IssuedAt = DateTime.UtcNow, IsActive = true };
        await _uow.QueueTickets.AddAsync(ticket, ct);
        await _uow.SaveChangesAsync(ct);
        return next;
    }

    private string GetNextQueue(string current)
    {
        if (current == "00" || current == "Z9") return "A0";
        char letter = current[0];
        int number = int.Parse(current[1].ToString());
        if (number < 9) return $"{letter}{number + 1}";
        char nextLetter = (char)(letter + 1);
        if (nextLetter > 'Z') nextLetter = 'A';
        return $"{nextLetter}0";
    }
}
