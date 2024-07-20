namespace Notebook.Services.EmailServices;

public sealed record SmtpOptions
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    
    public string Name { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
}