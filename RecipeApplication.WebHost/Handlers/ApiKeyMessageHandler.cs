namespace RecipeApplication.Handlers;

public class ApiKeyMessageHandler : DelegatingHandler
{
    private readonly IConfiguration _configuration;

    public ApiKeyMessageHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("X-API-KEY", _configuration.GetValue<string>("ApiKey"));
        
        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}