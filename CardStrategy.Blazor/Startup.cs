using CardStrategy.Blazor.ViewModels;
using CardStrategy.Core;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CardStrategy.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<StrategyViewModel, StrategyViewModel>();
            services.AddTransient<IRunAnalysis, RunAnalysis>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
