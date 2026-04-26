using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.Posts.Queries;

public record GetPostsQuery : IRequest<List<PostDto>>;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPostsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.Posts.GetQueryable()
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Author = p.Author,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return posts;
    }
}
