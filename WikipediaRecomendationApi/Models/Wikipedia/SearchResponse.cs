using System.Text.Json.Serialization;

namespace WikipediaRecomendationApi.Models.Wikipedia;

public class SearchResponse
{
    [JsonPropertyName("query")]
    public Query? Query { get; set; }
}

public class SearchResult
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("pageid")]
    public int PageId { get; set; }
}

public class Query
{
    [JsonPropertyName("search")]
    public IList<SearchResult>? Search { get; set; }
}
