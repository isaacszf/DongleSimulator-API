namespace DongleSimulator.Domain.Services.Tokens;

public interface ITokenProvider
{
    public string GetTokenOnRequest();
}