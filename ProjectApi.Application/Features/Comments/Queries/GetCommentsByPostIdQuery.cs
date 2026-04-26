using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.Comments.Queries;

public record GetCommentsByPostIdQuery(int PostId) : IRequest<List<CommentDto>>;

public class CommentDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string CommenterName { get; set; } = string.Empty;
    public string CommentText { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, List<CommentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCommentsByPostIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await _unitOfWork.Comments.GetQueryable()
            .Where(c => c.PostId == request.PostId)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                CommenterName = c.CommenterName,
                CommentText = c.CommentText,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return comments;
    }
}
