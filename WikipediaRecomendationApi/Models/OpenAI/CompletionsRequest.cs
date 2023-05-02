using System.Text.Json.Serialization;

namespace WikipediaRecomendationApi.Models.OpenAI;


    //generate c# class from json:
    //    {
    //  "model": "text-davinci-003",
    //  "prompt": "Say this is a test",
    //  "max_tokens": 7,
    //  "temperature": 0,
    //  "top_p": 1,
    //  "n": 1,
    //  "stream": false,
    //  "logprobs": null,
    //  "stop": "\n"
    //  }
    // use JsonPropertyNames to match the json property names
    // use JsonPropertyName to change the json property name
    // use JsonIgnore to ignore a property
    // use PacalCase for property names
    // use JsonConverter to convert a property
    // don't set values for properties
    // use nullable types for properties that can be null
public class CompletionsRequest
{

    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("prompt")]
    public string? Prompt { get; set; }
    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; set; }
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }
    [JsonPropertyName("n")]
    public int? N { get; set; }
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }
    [JsonPropertyName("logprobs")]
    public object? LogProbs { get; set; }
    [JsonPropertyName("stop")]
    public string? Stop { get; set; }
}
