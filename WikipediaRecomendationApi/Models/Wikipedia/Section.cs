using System.Text.Json.Serialization;

namespace WikipediaRecomendationApi.Models.Wikipedia;

public class Section
{
    [JsonPropertyName("line")]
    public string? Title { get; set; }
    [JsonPropertyName("index")]
    public int Index { get; set; }
}
