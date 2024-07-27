using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.RegistrationComplete;

public sealed class RegistrationCompleteHandler : IRequestHandler<RegistrationCompleteRequest, Result<string>>
{
    public Task<Result<string>> Handle(RegistrationCompleteRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}