using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.RegistrationComplete;

public sealed record RegistrationCompleteRequest(string Token, 
    string FirstName, 
    string LastName,
    string Login,
    string Password) : IRequest<Result<string>>;