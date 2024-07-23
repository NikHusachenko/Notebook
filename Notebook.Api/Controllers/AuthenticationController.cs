using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notebook.Api.ApiRequests.Authentication;
using Notebook.Handler.Authentication.Invite;

namespace Notebook.Api.Controllers
{
    [Route(AuthenticationControllerRoute)]
    public class AuthenticationController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost(InviteUserRoute)]
        public async Task<IActionResult> Invite([FromBody] InviteUserApiRequest request) =>
            await MapResult(new InviteRequest(request.Email));
    }
}