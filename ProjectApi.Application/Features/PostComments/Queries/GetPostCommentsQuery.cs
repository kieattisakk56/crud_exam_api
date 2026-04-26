using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.PostComments.Queries;

public class PostCommentDto { public int Id { get; set; } public string CommenterName { get; set; } = string.Empty; public string CommentText { get; set; } = string.Empty; }

public class GetPostCommentsQuery : IRequest<List<PostCommentDto>> { }

public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, List<PostCommentDto>>
{
    private readonly IUnitOfWork _uow;
    public GetPostCommentsQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<PostCommentDto>> Handle(GetPostCommentsQuery request, CancellationToken ct)
    {
        var items = await _uow.PostComments.GetAllAsync(ct);
        return items.OrderBy(c => c.Id).Select(c => new PostCommentDto { Id = c.Id, CommenterName = c.CommenterName, CommentText = c.CommentText }).ToList();
    }
}
