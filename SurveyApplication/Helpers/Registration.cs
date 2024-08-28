using System.Reflection;

namespace SurveyApplication.Helpers;
public static class Registration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
    }
}
