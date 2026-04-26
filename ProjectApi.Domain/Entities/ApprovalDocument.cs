using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 03: Document Approval System
/// Status: 0=Pending, 1=Approved, 2=Rejected
/// </summary>
public class ApprovalDocument : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public int Status { get; set; } = 0; // 0=Pending, 1=Approved, 2=Rejected
    public DateTime? ApprovedAt { get; set; }
    public string? ApprovedBy { get; set; }
}
