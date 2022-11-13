namespace RecipeApplication.SyncDataServices;

public interface IWikipediaClient
{
    Task<string> GetTitlesForTerm(string term);
}