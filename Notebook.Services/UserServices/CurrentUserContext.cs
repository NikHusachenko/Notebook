using Microsoft.AspNetCore.Http;

namespace Notebook.Services.UserServices;

public class CurrentUserContext : ICurrentUserContext
{
    private readonly HttpContext _httpContext;

    public CurrentUserContext(IHttpContextAccessor contextAccessor)
    {
        _httpContext = contextAccessor.HttpContext;
    }

    public Guid Id => throw new NotImplementedException();

    public string Login => throw new NotImplementedException();

    public string Email => throw new NotImplementedException();

    public string FirstName => throw new NotImplementedException();

    public string LastName => throw new NotImplementedException();
}