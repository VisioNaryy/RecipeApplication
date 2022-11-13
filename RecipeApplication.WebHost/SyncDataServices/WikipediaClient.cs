namespace RecipeApplication.SyncDataServices;

public class WikipediaClient : IWikipediaClient
{
    private readonly HttpClient _httpClient;

    public WikipediaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetTitlesForTerm(string term)
    {
        var queryParams = new Dictionary<string, string>() {{"action", "query"}, {"format", "json"}, {"list","search"}, {"utf8", "1"}, {"origin","*"}, {"srsearch", term}};

        var queryString = QueryString.Create(queryParams!).ToUriComponent();
        
        var response = await _httpClient.GetAsync(queryString);
        
        return await response.Content.ReadAsStringAsync();
    }
}