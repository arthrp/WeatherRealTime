using Microsoft.AspNetCore.SignalR;

namespace WeatherRealTime;

public class WeatherBackgroundService(ILogger<WeatherBackgroundService> _logger, IHubContext<WeatherHub> _hubContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var r = new Random();

        while (!stoppingToken.IsCancellationRequested)
        {
            var weather = GetInfo(r);
            _logger.LogInformation(weather.ToString());

            var group = weather.Location.ToString();
            await _hubContext.Clients.Group(group).SendCoreAsync("UpdateWeather", new[] { weather }, stoppingToken);
            
            await Task.Delay(1000, stoppingToken);
        }
    }

    private WeatherInfo GetInfo(Random r)
    {
        var loc = (Location)r.Next(0, 2);
        var speed = loc switch
        {
            Location.Paris => r.Next(0, 10),
            Location.Oslo => r.Next(10, 20),
            Location.Stockholm => r.Next(20, 30)
        };

        return new WeatherInfo(loc, speed);
    }
}