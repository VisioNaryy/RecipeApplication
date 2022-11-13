using RecipeApplication.Policies;

namespace RecipeApplication.SyncDataServices;

public class WikipediaClient : IWikipediaClient
{
    private readonly HttpClient _httpClient;
    private readonly IClientPolicy _clientPolicy;

    public WikipediaClient(HttpClient httpClient, IClientPolicy clientPolicy)
    {
        _httpClient = httpClient;
        _clientPolicy = clientPolicy;
    }

    public async Task<string> GetTitlesForTerm(string term)
    {
        var queryParams = new Dictionary<string, string>() {{"action", "query"}, {"format", "json"}, {"list","search"}, {"utf8", "1"}, {"origin","*"}, {"srsearch", term}};

        var queryString = QueryString.Create(queryParams!).ToUriComponent();
        
        var response = await _clientPolicy.ExponentialHttpRetry.ExecuteAsync(() => _httpClient.GetAsync(queryString));
        
        return await response.Content.ReadAsStringAsync();
    }
}