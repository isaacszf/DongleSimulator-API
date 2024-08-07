
namespace DongleSimulator.Domain.Services.Cryptography;

public interface IPasswordEncoder
{
    public string Encrypt(string password);
}