using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeatherRealTime.Controllers;

[Authorize]
public class WeatherController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        if (User.Claims.FirstOrDefault(x => x.Type == "location") == null)
        {
            return View("NoWeather");
        }
        
        return View();
    }
}