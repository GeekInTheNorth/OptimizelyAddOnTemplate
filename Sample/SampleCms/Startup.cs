using EPiServer.Cms.Shell;
using EPiServer.Cms.Shell.UI;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

using OptimizelyAddOn.ServiceExtensions;

using Stott.Optimizely.RobotsHandler.Configuration;
using Stott.Security.Optimizely.Features.Configuration;

namespace SampleCms;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;

    public Startup(IWebHostEnvironment webHostingEnvironment)
    {
        _webHostingEnvironment = webHostingEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (_webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }

        services.AddCmsAspNetIdentity<ApplicationUser>()
                .AddCms()
                .AddAdminUserRegistration(x => x.Behavior = RegisterAdminUserBehaviors.Enabled | RegisterAdminUserBehaviors.LocalRequestsOnly)
                .AddEmbeddedLocalization<Startup>();

        services.AddStottSecurity();
        services.AddRobotsHandler();
        services.AddOptimizelyAddOn();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStottSecurity();
        app.UseOptimizelyAddOn();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
            endpoints.MapControllers();
        });
    }
}
