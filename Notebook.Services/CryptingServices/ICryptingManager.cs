namespace Notebook.Services.CryptingServices;

public interface ICryptingManager
{
    string Encrypt(string value);
    string Decrypt(string value);
}