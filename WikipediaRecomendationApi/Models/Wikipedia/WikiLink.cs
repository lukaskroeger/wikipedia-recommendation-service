using System.Text.Json.Serialization;

namespace WikipediaRecomendationApi.Models.Wikipedia;

public class WikiLink
{
    [JsonPropertyName("*")]
    public string PageTitle { get; set; }

    [JsonPropertyName("ns")]
    public int Namespace { get; set; }
}
