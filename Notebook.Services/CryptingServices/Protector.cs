using Microsoft.AspNetCore.DataProtection;

namespace Notebook.Services.CryptingServices;

public sealed class Protector : ICryptingManager
{
    private readonly IDataProtector _protector;

    public Protector(IDataProtectionProvider protectionProvider)
    {
        _protector = protectionProvider.CreateProtector("note-protector");
    }

    public string Encrypt(string value) => _protector.Protect(value);

    public string Decrypt(string value) => _protector.Unprotect(value);
}