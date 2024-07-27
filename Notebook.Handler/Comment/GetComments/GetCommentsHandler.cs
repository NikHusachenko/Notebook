using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.Extensions;

namespace Notebook.Handler.Comment.GetComments;

public sealed class GetCommentsHandler(IGenericRepository<CommentEntity> repository)
    : IRequestHandler<GetCommentsRequest, List<CommentEntity>>
{
    public async Task<List<CommentEntity>> Handle(GetCommentsRequest request, CancellationToken cancellationToken) =>
        await repository.GetAll(comment => comment.NoteId == request.Filter.NoteId)
            .Paging(request.Filter.Take, request.Filter.Page);
}