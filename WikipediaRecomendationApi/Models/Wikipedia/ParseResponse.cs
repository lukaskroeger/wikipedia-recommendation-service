using System.Text.Json.Serialization;

namespace WikipediaRecomendationApi.Models.Wikipedia;

public class ParseResponse
{
    [JsonPropertyName("parse")]
    public Parse? Parse { get; set; }
}
