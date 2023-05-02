using Microsoft.AspNetCore.Mvc;
using WikipediaRecomendationApi.Models;
using WikipediaRecomendationApi.Services;

namespace WikipediaRecomendationApi.Controllers;
public class OpenAIController : ControllerBase
{
    ILogger<OpenAIController> _logger;

    OpenAIService openAiSerivce;
    IWikipediaService _wikipediaService;

    public OpenAIController(ILogger<OpenAIController> logger, OpenAIService openAiService, IWikipediaService wikipediaService)
    {
        _logger = logger;
        openAiSerivce = openAiService;
        _wikipediaService = wikipediaService;
    }

    [HttpGet("/openai/{pageTitle}")]
    public async Task<ActionResult<IEnumerable<RelatedArticle>>> GetFromOpenAiModel(string pageTitle, [FromQuery] string? language = null)
    {
        var selectedLanguage = SupportedLanguage.en;
        var recommendedTopics = await openAiSerivce.GetRelatedTopics(pageTitle, selectedLanguage);
        _logger.LogInformation($"Recommended Topics from GPT: {string.Join(",", recommendedTopics)}");
        List<RelatedArticle> result = new List<RelatedArticle>();
        foreach(var topic in recommendedTopics)
        {
            var article = await _wikipediaService.GetSearchResultPageTitle(topic, selectedLanguage);
            if(article != null)
            {
                result.Add(new RelatedArticle() { Title = article });
            }
        }
        return Ok(result);
    }
}
