using MediatR;
using ProjectApi.Domain.Common;

namespace ProjectApi.Application.Features.QueueTickets.Commands;

public class ClearQueueCommand : IRequest<bool> { }

public class ClearQueueCommandHandler : IRequestHandler<ClearQueueCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public ClearQueueCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<bool> Handle(ClearQueueCommand request, CancellationToken ct)
    {
        var active = await _uow.QueueTickets.FindAsync(q => q.IsActive, ct);
        foreach (var t in active)
        {
            t.IsActive = false;
            _uow.QueueTickets.Update(t);
        }
        await _uow.SaveChangesAsync(ct);
        return true;
    }
}
