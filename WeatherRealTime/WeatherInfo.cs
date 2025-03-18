namespace WeatherRealTime;

public enum Location
{
    Paris, Oslo, Stockholm
}

public record WeatherInfo(Location Location, int WindSpeed);