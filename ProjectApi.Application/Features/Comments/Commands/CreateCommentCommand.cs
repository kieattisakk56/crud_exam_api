using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.Comments.Commands;

public record CreateCommentCommand(int PostId, string CommenterName, string CommentText) : IRequest<int>;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            PostId = request.PostId,
            CommenterName = request.CommenterName,
            CommentText = request.CommentText
        };

        await _unitOfWork.Comments.AddAsync(comment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
