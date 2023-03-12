using Microsoft.AspNetCore.Mvc;
using WikipediaRecomendationApi.Models;
using WikipediaRecomendationApi.Services;

namespace WikipediaRecomendationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    private readonly ILogger<LinksController> _logger;
    private readonly IWikipediaService _wikipediaService;

    public LinksController(ILogger<LinksController> logger, IWikipediaService wikipediaService)
    {
        _logger = logger;
        _wikipediaService = wikipediaService;
    }

    [HttpGet("seealso/{pageTitle}")]
    public async Task<ActionResult<IEnumerable<RelatedArticle>>> GetDependingOnSeeAlso(string pageTitle, [FromQuery] string? language = null)
    {
        var selectedLanguage = SupportedLanguage.en;
        if (language is null || Enum.TryParse(language, out selectedLanguage))
        {
            IEnumerable<RelatedArticle> result = (await _wikipediaService.GetSeeAlsoLinks(pageTitle, selectedLanguage)).Select(x => new RelatedArticle() { Title = x });
            return Ok(result);

        }
        return BadRequest($"Wrong query paramerter. Please specify one of the following languages: {string.Join(',', Enum.GetValues<SupportedLanguage>())}");
    }
}
