namespace WeatherRealTime;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton<IIdentityService, IdentityService>();

        builder.Services.AddAuthentication()
            .AddCookie("Cookie", o =>
            {
                o.LoginPath = "/Account/Login";
                o.LogoutPath = "/Account/Logouit";
            });

        builder.Services.AddSignalR();
        builder.Services.AddHostedService<WeatherBackgroundService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        
        app.MapHub<WeatherHub>("/weatherHub");

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}