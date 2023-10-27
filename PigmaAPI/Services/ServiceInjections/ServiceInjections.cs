using PigmaAPI.Services.CompanyContacts.Services;
using PigmaAPI.Services.CompanyContacts.Services.Contracts;

namespace PigmaAPI.Services.ServiceInjections
{
    public static class ServiceInjections
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICompanyContactService, CompanyContactService>();
        }
    }
}
