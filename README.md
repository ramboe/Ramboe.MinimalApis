# Ramboe.MinimalApis

## Description  

With this package you can outsource your asp.net core minimal api endpoints to dedicated classes to keep the Program.cs clean. It provides an interface IEndpoints that simply has to be implemented by your class:  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FnE_iGpqYP4.png?alt=media&token=5690f046-beaf-4277-82c9-b41774a825c9)  

When starting up the web service, all URLs of the IEndpoints are automatically mapped and all endpoints are supplied with the services from the Dependency Injection Container:  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FAg2gdzqsA5.png?alt=media&token=85136087-95f9-4f47-8463-11d0898d31d6)  

## Installation  

### Create an empty ASP.NET Core project with the editor of your choice  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2Fmi-3zx8dxW.png?alt=media&token=c2a58ad5-8aae-452b-9b87-a09255c7a3b3)  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FxgU88E7158.png?alt=media&token=ad283b3b-9941-4e52-ba1c-ed773620d0e2)  

should look something like this  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2Fxvn0jg9XSj.png?alt=media&token=9e98bfa6-4a0a-4b28-9454-d6ee9c7fc825)  

### Install the nuget package 'Ramboe.MinimalApis' and implement 'IEndpoints'  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FAj1dYz0uQQ.png?alt=media&token=2e89c64a-5510-45a6-8e72-7981f6f02644)  

Within the ASP.NET Core Project, create a folder 'Endpoints' and inside that folder create a class 'ExampleEndpints.cs'  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FnE_iGpqYP4.png?alt=media&token=5690f046-beaf-4277-82c9-b41774a825c9)  

Then implement the 'IEndpoints' from the namespace 'Ramboe.MinimalApis'  

```c#
using Ramboe.MinimalApis;

namespace WebApplication1.Endpoints
{
    public class ExampleEndpoints : IEndpoints
    {
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => "hello from example endpoints");
            app.MapGet("/stuff", doStuff);
        }

        public static void AddServices(IServiceCollection services, IConfiguration configuration, bool isDev)
        {
            //services that these endpoints require
        }

        static IResult doStuff(HttpContext context)
        {
            var body = context.Request.Body.ToString();


            return Results.Ok(body);
        }
    }
}
```  

### Organize the startup  

Inside Program.cs add the Endpoints to the services by using the folling lines  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FAg2gdzqsA5.png?alt=media&token=85136087-95f9-4f47-8463-11d0898d31d6)  

```c#
using Ramboe.MinimalApis;

var builder = WebApplication.CreateBuilder(args);

region insert
var isDev = builder.Environment.IsDevelopment();
builder.Services.AddEndpoints<Program>(builder.Configuration, isDev);
endregion

var app = builder.Build();

app.UseEndpoints<Program>(); //insert

app.Run();
```

The AddEndpoints() method makes the services from the dependency injection container (e.g. your DbContext) available to your IEndpoints.

The UseEndpoints() method executes the DefineEndpoints() method of each class that implements IEndpoints interface.

## Test  

Start your webservice to test if everything worked  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FiHDQ2AKdFF.png?alt=media&token=b9939965-ffb4-4877-9729-37cc904198c9)  
