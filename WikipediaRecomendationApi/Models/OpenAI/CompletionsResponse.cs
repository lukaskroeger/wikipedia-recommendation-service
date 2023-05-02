using System.Text.Json.Serialization;

namespace WikipediaRecomendationApi.Models.OpenAI;

public class CompletionsResponse
{

    //generate a c# type from json:
    //    {
    //  "id": "cmpl-uqkvlQyYK7bGYrRHQ0eXlWi7",
    //  "object": "text_completion",
    //  "created": 1589478378,
    //  "model": "text-davinci-003",
    //  "choices": [
    //    {
    //      "text": "\n\nThis is indeed a test",
    //      "index": 0,
    //      "logprobs": null,
    //      "finish_reason": "length"
    //    }
    //  ],
    //  "usage": {
    //    "prompt_tokens": 5,
    //    "completion_tokens": 7,
    //    "total_tokens": 12
    //  }
    //}
    // use JsonPropertyNames to match the json property names
    // use JsonPropertyName to change the json property name
    // use JsonIgnore to ignore a property
    // use PacalCase for property names
    // use JsonConverter to convert a property
    // don't set values for properties
    // use nullable types for properties that can be null
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("object")]
    public string Object { get; set; }
    [JsonPropertyName("created")]
    public int Created { get; set; }
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; } = new List<Choice>();
    [JsonPropertyName("usage")]
    public Usage Usage { get; set; } = new Usage();
}


//create a class for Choice form the json above
public class Choice
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
    [JsonPropertyName("index")]
    public int Index { get; set; }
    [JsonPropertyName("logprobs")]
    public object? LogProbs { get; set; }
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }
}

//create a class for Usage form the json above
public class Usage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
}

