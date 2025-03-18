using System.Security.Claims;

namespace WeatherRealTime;

public interface IIdentityService
{
    bool AreCredentialsValid(string username, string password);
    List<Claim> GetUserClaims(string username);
}