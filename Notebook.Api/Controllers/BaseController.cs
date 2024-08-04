using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notebook.Services.Extensions;
using Notebook.Services.ResultService;

namespace Notebook.Api.Controllers;

[ApiController]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected const string AuthenticationControllerRoute = "api/authentication";
    protected const string InviteUserRoute = "invite";
    protected const string RegistrationCompleteRoute = "registration-complete";
    protected const string SignInRoute = "sign-in";

    protected const string NoteControllerRoute = "api/note";

    protected const string CreateBaseRoute = "create";
    protected const string GetAllBaseRoute = "get/all";
    protected const string GetByIdBaseRoute = "get/{id:guid}";
    protected const string DeleteBaseRoute = "delete";

    protected async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request) => await mediator.Send(request);
    protected IActionResult AsSuccess() => NoContent();
    protected IActionResult AsSuccess<T>(Result<T> result) => Ok(result.Value);
    protected IActionResult AsSuccess<T>(ICollection<T> result) => Ok(result);

    protected IActionResult AsError(string errorMessage) =>
        BadRequest(new { errorMessage = errorMessage });

    protected IActionResult AsError(List<string> errorMessages) =>
        BadRequest(new { errorMessages = errorMessages });

    protected IActionResult AsNotFound(string? errorMessage) =>
        NotFound(string.IsNullOrEmpty(errorMessage) ? null : new
        {
            errorMessage = errorMessage
        });

    protected async Task<IActionResult> MapResult(IRequest<Result> arg) =>
        await SendRequest(arg).Map(result =>
            result.ErrorMessages.Any() ?
                AsError(result.ErrorMessages) :
                AsSuccess());

    protected async Task<IActionResult> MapResult<TResult>(IRequest<Result<TResult>> arg) =>
        await SendRequest(arg).Map(result =>
            result.ErrorMessages.Any() ?
                AsError(result.ErrorMessages) :
                AsSuccess());

    protected async Task<IActionResult> MapResult<TResponse>(IRequest<ICollection<TResponse>> arg) =>
        await SendRequest(arg).Map(result =>
            result.Any() ? AsSuccess(result) : AsSuccess());
}