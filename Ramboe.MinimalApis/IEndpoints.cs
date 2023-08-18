using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ramboe.MinimalApis;

public interface IEndpoints
{
    public abstract static void DefineEndpoints(IEndpointRouteBuilder app);

    public abstract static void AddServices(IServiceCollection services, IConfiguration configuration, bool isDev);
}
