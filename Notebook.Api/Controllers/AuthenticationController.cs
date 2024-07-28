using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notebook.Api.ApiRequests.Authentication;
using Notebook.Handler.Authentication.Invite;
using Notebook.Services.CacheService;

namespace Notebook.Api.Controllers
{
    [Route(AuthenticationControllerRoute)]
    public class AuthenticationController(IMediator mediator,
        ICacheManager manager) : BaseController(mediator)
    {
        [HttpPost(InviteUserRoute)]
        public async Task<IActionResult> Invite([FromBody] InviteUserApiRequest request) =>
            await MapResult(new InviteRequest(request.Email));

        [HttpPost(RegistrationCompleteRoute)]
        public async Task<IActionResult> RegistrationComplete([FromQuery] string token) => NotFound();

        [HttpGet("get-value")]
        public async Task<IActionResult> GetValue()
        {
            return Ok(await manager.Get("key"));
        }
    }
}