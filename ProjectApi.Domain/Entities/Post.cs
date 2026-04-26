using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 09: Post System
/// Represents a post that can have multiple comments
/// </summary>
public class Post : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
