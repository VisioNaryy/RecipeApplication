using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using RecipeApplication.Middleware;

namespace RecipeApplication.Tests;

public class StatusMiddlewareTests
{
    [Fact]
    public async Task ForNonMatchingRequest_CallsNextDelegate()
    {
        var context = new DefaultHttpContext();
        context.Request.Path = "/somethingelse";

        var wasExecuted = false;

        RequestDelegate next = (HttpContext _) =>
        {
            wasExecuted = true;
            return Task.CompletedTask;
        };

        var middleware = new StatusMiddleware(next);

        await middleware.Invoke(context);
        
        Assert.True(wasExecuted);
    }

    [Fact]
    public async Task ReturnsPongBodyContent()
    {
        using (var bodyStream = new MemoryStream())
        {
            var context = new DefaultHttpContext();
            
            context.Response.Body = bodyStream;
            context.Request.Path = "/ping";
            
            RequestDelegate next = (ctx) => Task.CompletedTask;
            
            var middleware = new StatusMiddleware(next: next);
            await middleware.Invoke(context);
            
            string response;
            bodyStream.Seek(0, SeekOrigin.Begin);
            using (var stringReader = new StreamReader(bodyStream))
            {
                response = await stringReader.ReadToEndAsync();
            }
            
            Assert.Equal("pong", response);
            Assert.Equal("text/plain", context.Response.ContentType);
            Assert.Equal(200, context.Response.StatusCode);
        }
    }

    [Fact]
    public async Task StatusMiddlewareReturnsPong()
    {
        var hostBuilder = new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                webHost.Configure(app =>
                    app.UseMiddleware<StatusMiddleware>());
                webHost.UseTestServer();
            });
        var host = await hostBuilder.StartAsync();
        var client = host.GetTestClient();
        
        var response = await client.GetAsync("/ping");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Equal("pong", content);
    }
}