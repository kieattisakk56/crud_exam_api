using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 05: Queue Ticket System
/// Running A0 - Z9, resets on clear
/// </summary>
public class QueueTicket : BaseEntity
{
    public string TicketNumber { get; set; } = string.Empty;   // e.g. "A5", "B0"
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;                 // false after clear
}
