using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using AzureFunctionsWithAad;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureFunctionsWithAad
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddMicrosoftIdentityWebApi(configuration);
        }
    }
}