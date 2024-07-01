using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.SignIn;

public sealed record SignInRequest(string login, string password) : IRequest<Result<string>>;