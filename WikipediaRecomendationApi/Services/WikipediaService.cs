using Microsoft.AspNetCore.Http.Extensions;
using WikipediaRecomendationApi.Models.Wikipedia;
using System.Text.Json;
using WikipediaRecomendationApi.Models;

namespace WikipediaRecomendationApi.Services;

public class WikipediaService : IWikipediaService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WikipediaService> _logger;

    public WikipediaService(IConfiguration config, HttpClient httpClient, ILogger<WikipediaService> logger)
    {
        _ = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _ = config ?? throw new ArgumentNullException(nameof(config));

        _configuration = config;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<string>> GetSeeAlsoLinks(string pageTilte, SupportedLanguage language)
    {
        _httpClient.BaseAddress = new Uri($"https://{language}.{_configuration["Wikipedia:ApiBaseUrl"]}");
        Parse? pageParse = await GetPageParse(pageTilte);
        Section? seeAlsoSection = pageParse?.Sections?.FirstOrDefault(x => x.Title == _configuration[$"Wikipedia:SeeAlsoSectionNames:{language}"]);
        if (seeAlsoSection is null)
        {
            return Enumerable.Empty<string>();
        }
        Parse? sectionParse = await GetSectionParse(pageTilte, seeAlsoSection.Index);
        return sectionParse?.Links?.Where(x => x.Namespace == 0).Select(x => x.PageTitle) ?? Enumerable.Empty<string>();
    }
       

    private async Task<Parse?> GetPageParse(string pageTitle)
    {
        ParseResponse? result = null;
        QueryBuilder qb = new();
        qb.Add("format", "json");
        qb.Add("action", "parse");
        qb.Add("page", pageTitle);
        using HttpResponseMessage response = await _httpClient.GetAsync(qb.ToQueryString().ToUriComponent());
        if (response.IsSuccessStatusCode)
        {
            try
            {
                result = await response.Content.ReadFromJsonAsync<ParseResponse>();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Json Content: \n {0}", await response.Content.ReadAsStringAsync());
            }
        }
        return result?.Parse;
    }

    private async Task<Parse?> GetSectionParse(string pageTitle, int sectionId)
    {
        ParseResponse? result = null;
        QueryBuilder qb = new();
        qb.Add("action", "parse");
        qb.Add("format", "json");
        qb.Add("page", pageTitle);
        qb.Add("section", sectionId.ToString());
        using HttpResponseMessage response = await _httpClient.GetAsync(qb.ToQueryString().ToUriComponent());
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<ParseResponse>();
        }
        return result?.Parse;
    }
}