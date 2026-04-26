using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 09: Comment System
/// Comments posted under a single post feed by commenter "Blend 285"
/// </summary>
public class PostComment : BaseEntity
{
    public string CommenterName { get; set; } = string.Empty;
    public string CommentText { get; set; } = string.Empty;
}
