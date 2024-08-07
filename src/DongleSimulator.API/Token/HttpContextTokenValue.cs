using DongleSimulator.Domain.Services.Tokens;

namespace DongleSimulator.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
        => _contextAccessor = httpContextAccessor;
    
    public string GetTokenOnRequest()
    {
        var auth = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return auth.Replace("Bearer ", "").Trim();
    }
}