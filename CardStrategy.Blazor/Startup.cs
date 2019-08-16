using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CardStrategy.Core;
using CardStrategy.Blazor.Helpers;
using CardStrategy.Core.ViewModels;

namespace CardStrategy.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainViewModel, MainViewModel>();
            services.AddTransient<IRunAnalysis, RunAnalysis>();
            services.AddTransient<IPlayHand, PlayHand>();
            services.AddTransient<ILogger, ScreenLogger<MainViewModel>>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");            
        }
    }
}
