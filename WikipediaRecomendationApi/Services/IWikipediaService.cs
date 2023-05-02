using WikipediaRecomendationApi.Models;

namespace WikipediaRecomendationApi.Services;

public interface IWikipediaService
{
    public Task<IEnumerable<string>> GetSeeAlsoLinks(string pageTilte, SupportedLanguage language);

    public Task<string?> GetSearchResultPageTitle(string searchQuery, SupportedLanguage language);

}
