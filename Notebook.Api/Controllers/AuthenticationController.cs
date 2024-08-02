using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notebook.Api.ApiRequests.Authentication;
using Notebook.Api.MiddleWares;
using Notebook.Handler.Authentication.Invite;
using Notebook.Handler.Authentication.RegistrationComplete;
using Notebook.Services.AuthenticationServices;

namespace Notebook.Api.Controllers
{
    [Route(AuthenticationControllerRoute)]
    public class AuthenticationController(IMediator mediator,
        ISessionManager manager) : BaseController(mediator)
    {
        [HttpPost(InviteUserRoute)]
        public async Task<IActionResult> Invite([FromBody] InviteUserApiRequest request) =>
            await MapResult(new InviteRequest(request.Email));

        [HttpPost(RegistrationCompleteRoute)]
        public async Task<IActionResult> RegistrationComplete(
            [FromQuery] string token, 
            [FromBody] RegistrationCompleteApiRequest request) =>
            await MapResult(new RegistrationCompleteRequest(token, request.FirstName, request.LastName, request.LastName, request.Password));

        [HttpGet("test")]
        [SessionAuthorize]
        public async Task Test() => Ok();
    }
}