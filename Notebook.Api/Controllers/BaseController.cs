using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notebook.Services.Extensions;
using Notebook.Services.ResultService;

namespace Notebook.Api.Controllers;

[ApiController]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected const string AuthenticationControllerRoute = "api/authentication";
    protected const string InviteUserRoute = "/invite";
    protected const string RegistrationCompleteRoute = "/registration-complete";

    protected async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request) => await mediator.Send(request);
    protected IActionResult AsSuccess() => NoContent();
    protected IActionResult AsSuccess<T>(Result<T> result) => Ok(result.Value);

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
}