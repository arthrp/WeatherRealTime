using System.Security.Claims;

namespace WeatherRealTime.Models;

public class UserInfoModel
{
    public IEnumerable<Claim> Claims { get; set; } = [];
}