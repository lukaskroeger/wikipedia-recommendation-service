using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using WikipediaRecomendationApi.Models;
using WikipediaRecomendationApi.Models.OpenAI;

namespace WikipediaRecomendationApi.Services;

public class OpenAIService
{
    private readonly ILogger<OpenAIService> _logger;
    private readonly HttpClient _httpClient;
    public OpenAIService(HttpClient httpClient, ILogger<OpenAIService> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    private readonly string recommendWikipediaArticlesPromt = @"Recommend five wikipedia article titles to a person that likes the article about {0}. Do not include the article the user already likes. Put the output in the following structure:
'''
[""Title"", ""Title"", ...]
'''";


    public async Task<IEnumerable<string>> GetRelatedTopics(string pageTitle, SupportedLanguage language)
    {
        return await MakeOpenAiRequest(GetPromt(pageTitle, language), "text-davinci-003", 0)?? Enumerable.Empty<string>(); 
    }

    private string GetPromt(string pageTitle, SupportedLanguage language)
    {
        return string.Format(recommendWikipediaArticlesPromt, pageTitle);
    }

    private async Task<List<string>?> MakeOpenAiRequest(string promt, string model, int temperature)
    {
        var payload = new CompletionsRequest()
        {
            Prompt = promt,
            Temperature = temperature,
            MaxTokens = 256,
            Model = model
        };

        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Environment.GetEnvironmentVariable("OPEN_AI_KEY"));
        var json = JsonSerializer.Serialize(payload);
        var content  = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);
        if(response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CompletionsResponse>(responseContent);
            string gptAnswer = result?.Choices?.FirstOrDefault()?.Text ?? "";
            gptAnswer = gptAnswer.Trim();
            if (!ValidateResponseStructure(gptAnswer))
            {
                _logger.LogError("Invalid response structure from OpenAI: " + responseContent);
                return null;
            }
            return JsonSerializer.Deserialize<List<string>>(gptAnswer);
        }
        _logger.LogError(await response.Content.ReadAsStringAsync());
        return null;
    }

    private bool ValidateResponseStructure(string response)
    {
        var regex = new Regex(@"\[(\"".*\""\,*)+\]");
        return regex.IsMatch(response);
    }
}
