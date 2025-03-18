using Microsoft.AspNetCore.SignalR;

namespace WeatherRealTime;

public class WeatherHub : Hub
{
    public async Task JoinGroup()
    {
        if (!(Context?.User?.Identity?.IsAuthenticated) ?? false) return;

        var locationClaim = Context.User.Claims.FirstOrDefault(x => x.Type == "location");

        if (locationClaim == null) return;

        await Groups.AddToGroupAsync(Context.ConnectionId, locationClaim.Value);
    }
}