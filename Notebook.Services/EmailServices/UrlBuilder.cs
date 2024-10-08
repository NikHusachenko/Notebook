﻿using Microsoft.AspNetCore.Http;

namespace Notebook.Services.EmailServices;

public sealed class UrlBuilder
{
    private const string AuthenticationController = "api/authentication";
    private const string ConfirmInviteTokenAction = "registration-complete";
    private const string TokenParamName = "token";

    private readonly HttpContext _httpContext;
    
    private string Scheme => _httpContext.Request.Scheme;
    private string Domain => _httpContext.Request.Host.Value;

    public UrlBuilder(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public string BuildInviteUrl(string token) =>
        $"{Scheme}://{Domain}/{AuthenticationController}/{ConfirmInviteTokenAction}?{TokenParamName}={token}";
}