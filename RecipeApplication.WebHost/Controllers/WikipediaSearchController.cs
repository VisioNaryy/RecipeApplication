using Microsoft.AspNetCore.Mvc;
using RecipeApplication.SyncDataServices;

namespace RecipeApplication.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class WikipediaSearchController : ControllerBase
{
    private readonly IWikipediaClient _wikipediaClient;
    private readonly IConfiguration _configuration;

    public WikipediaSearchController(IWikipediaClient wikipediaClient, IConfiguration configuration)
    {
        _wikipediaClient = wikipediaClient;
        _configuration = configuration;
    }

    [HttpGet("{term}")]
    public async Task<IActionResult> GetTitles(string term)
    {
        var response = await _wikipediaClient.GetTitlesForTerm(term);

        return Ok(response);
    }
}