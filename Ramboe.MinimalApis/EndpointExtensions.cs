using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ramboe.MinimalApis;

public static class EndpointExtensions
{
    public static void AddEndpoints<TMarker>(this IServiceCollection services, IConfiguration configuration, bool isDev)
    {
        AddEndpoints(services, configuration, isDev, typeof(TMarker));
    }

    public static void AddEndpoints(this IServiceCollection services, IConfiguration configuration, bool isDev, Type marker)
    {
        var endpointTypes = getEndpointTypesFromAssembly(marker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.AddServices))!.Invoke(null, new object[] {services, configuration, isDev});
        }
    }

    public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
    {
        UseEndpoints(app, typeof(TMarker));
    }

    public static void UseEndpoints(this IApplicationBuilder app, Type marker)
    {
        var endpointTypes = getEndpointTypesFromAssembly(marker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!.Invoke(null, new object[] {app});
        }
    }

    static IEnumerable<TypeInfo> getEndpointTypesFromAssembly(Type marker)
    {
        var endpointTypes = marker.Assembly.DefinedTypes
                                  .Where(x => !x.IsAbstract
                                              && !x.IsInterface
                                              && typeof(IEndpoints).IsAssignableFrom(x));

        return endpointTypes;
    }
}
