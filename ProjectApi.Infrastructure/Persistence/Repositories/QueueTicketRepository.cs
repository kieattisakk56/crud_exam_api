using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class QueueTicketRepository : Repository<QueueTicket>, IQueueTicketRepository
{
    public QueueTicketRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
