namespace Ramboe.MinimalApis.Testing.Endpoints;

public class LoginEndpoints : IEndpoints
{
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        var ENDPOINT_URL = "login";

        app.MapGet(ENDPOINT_URL, DoSomething)
           .WithTags(ENDPOINT_URL)
           .Produces(200)
           .WithName("ResendActivationLink");
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration, bool isDev)
    {
        // no special services needed here
    }

    internal static IResult DoSomething(HttpContext context, string userid, LinkGenerator linker)
    {
        var user = new
        {
            UserId = userid
        };

        return Results.Ok(user);
    }
}
