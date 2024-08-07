using System.Text.RegularExpressions;

namespace DongleSimulator.Application.UseCases.User.Register;

public static class UserHelperValidator
{
    private const int MinUsernameLength = 5;
    private const int MaxUsernameLength = 20;
    private const int MinPasswordLength = 6;
    private const int MaxPasswordLength = 100;

    public static bool IsUsernameValid(string username)
    {
        if (username.Length is > MaxUsernameLength or < MinUsernameLength) return false;
        
        var pattern = @"^[a-zA-Z0-9_]+$";
        return Regex.IsMatch(username, pattern);
    }

    public static bool IsPasswordValid(string password)
        => password.Length is <= MaxPasswordLength and >= MinPasswordLength;
    
    public static bool IsEmailValid(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith('.')) {
            return false;
        }
        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch {
            return false;
        }
    }
}