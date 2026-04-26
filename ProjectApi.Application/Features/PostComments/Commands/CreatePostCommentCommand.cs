using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.PostComments.Commands;

public class CreatePostCommentCommand : IRequest<int>
{
    public string CommenterName { get; set; } = string.Empty;
    public string CommentText { get; set; } = string.Empty;
}

public class CreatePostCommentCommandHandler : IRequestHandler<CreatePostCommentCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePostCommentCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreatePostCommentCommand request, CancellationToken ct)
    {
        var item = new PostComment { CommenterName = request.CommenterName, CommentText = request.CommentText };
        await _uow.PostComments.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
