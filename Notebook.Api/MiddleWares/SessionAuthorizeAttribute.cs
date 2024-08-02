using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Notebook.Services.AuthenticationServices;
using Notebook.Services.ResultService;
using System.Security.Claims;

namespace Notebook.Api.MiddleWares;

internal sealed class SessionAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    private const string SESSION_KEY_NAME = "Id";

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ISessionManager manager = context.HttpContext.RequestServices.GetRequiredService<ISessionManager>();

        Result<string> authResult = manager.Get(SESSION_KEY_NAME);
        if (authResult.ErrorMessages.Any())
        {
            context.Result = new UnauthorizedResult();
            return Task.CompletedTask;
        }

        context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(SESSION_KEY_NAME, authResult.Value)]
        ));

        return Task.CompletedTask;
    }
}