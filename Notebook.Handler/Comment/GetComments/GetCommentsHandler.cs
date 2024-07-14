using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;

namespace Notebook.Handler.Comment.GetComments;

public sealed class GetCommentsHandler(
    IRepositoryFactory repositoryFactory)
    : IRequestHandler<GetCommentsRequest, List<CommentEntity>>
{
    public async Task<List<CommentEntity>> Handle(GetCommentsRequest request, CancellationToken cancellationToken)
    {
        CommentRepository repository = repositoryFactory.NewCommentRepository();
        return await repository.GetComments(request.Filter.NoteId, request.Filter.Page, request.Filter.Take);
    }
}