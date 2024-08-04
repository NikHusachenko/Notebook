using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notebook.Api.ApiRequests.Note;
using Notebook.Api.MiddleWares;
using Notebook.Handler.Models;
using Notebook.Handler.Note.GetNotes;
using Notebook.Handler.Note.NewNote;

namespace Notebook.Api.Controllers;

[SessionAuthorize]
[Route(NoteControllerRoute)]
public sealed class NoteController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost(CreateBaseRoute)]
    public async Task<IActionResult> NewNote([FromBody] NewNoteApiRequest request) =>
        await MapResult(new NewNoteRequest(request.Content));

    [HttpGet(GetAllBaseRoute)]
    public async Task<IActionResult> GetAll([FromQuery] GetNotesFilter filter) =>
        await MapResult(new GetNotesRequest(filter));
}