# Ramboe.MinimalApis

## Description  

With this package you can outsource your asp.net core minimal api endpoints to dedicated classes to keep the Program.cs clean. It provides an interface IEndpoints that simply has to be implemented by your class:  

![](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/nE_iGpqYP4.png)  

When starting up the web service, all URLs of the IEndpoints are automatically mapped and all endpoints are supplied with the services from the Dependency Injection Container:  

![]([https://clemenskrusenetmedia.blob.core.windows.net/github/Ag2gdzqsA5.png](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/Ag2gdzqsA5.png)

## Installation  

### Create an empty ASP.NET Core project with the editor of your choice  

![](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/mi-3zx8dxW.png)  

![](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/xgU88E7158.png)  

should look something like this  

![](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/xvn0jg9XSj.png)  

### Install the nuget package 'Ramboe.MinimalApis' and implement 'IEndpoints'  

![](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/Aj1dYz0uQQ.png)  

Within the ASP.NET Core Project, create a folder 'Endpoints' and inside that folder create a class 'ExampleEndpints.cs'  

![](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/xvn0jg9XSj.png)  

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

![]([https://clemenskrusenetmedia.blob.core.windows.net/github/Ag2gdzqsA5.png](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/Ag2gdzqsA5.png)  

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

Start your webservice to test if everything worked:  

![](https://raw.githubusercontent.com/ramboe/Ramboe.MinimalApis/193065fec7b6f68139cd3a433abfa8583a2a3240/images/iHDQ2AKdFF.png)  
