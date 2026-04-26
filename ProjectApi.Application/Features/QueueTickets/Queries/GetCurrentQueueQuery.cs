using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.QueueTickets.Queries;

public class GetCurrentQueueQuery : IRequest<string> { }

public class GetCurrentQueueQueryHandler : IRequestHandler<GetCurrentQueueQuery, string>
{
    private readonly IUnitOfWork _uow;
    public GetCurrentQueueQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<string> Handle(GetCurrentQueueQuery request, CancellationToken ct)
    {
        var active = await _uow.QueueTickets.FindAsync(q => q.IsActive, ct);
        var latest = active.OrderByDescending(q => q.Id).FirstOrDefault();
        return latest?.TicketNumber ?? "00";
    }
}
