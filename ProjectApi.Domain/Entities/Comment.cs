using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 09: Comment System
/// Represents a comment attached to a specific Post
/// </summary>
public class Comment : BaseEntity
{
    public int PostId { get; set; }
    public string CommenterName { get; set; } = string.Empty;
    public string CommentText { get; set; } = string.Empty;

    // Navigation property
    public Post? Post { get; set; }
}
