using System.Security.Claims;

namespace WeatherRealTime;

public class IdentityService : IIdentityService
{
    private readonly List<string> _allowedUsers = new()
    {
        "alice", "bob", "john", "jane", "grzegorz"
    };
    
    public bool AreCredentialsValid(string username, string password)
    {
        var areValid = !string.IsNullOrWhiteSpace(username) &&
                       !string.IsNullOrWhiteSpace(password) &&
                       _allowedUsers.Contains(username) &&
                       username == password;
        return areValid;
    }

    public List<Claim> GetUserClaims(string username)
    {
        var res = username switch
        {
            "alice" => new List<Claim>()
            {
                new Claim("location", "Paris")
            },
            "bob" => new List<Claim>()
            {
                new Claim("location", "Oslo")
            },
            "john" => new List<Claim>()
            {
                new Claim("location", "Oslo")
            },
            "jane" => new List<Claim>()
            {
                new Claim("location", "Oslo")
            },
            _ => new List<Claim>()
        };

        res.Add(new Claim("name", char.ToUpper(username[0]) + username[1..]));
        return res;
    }
}