using System.Text.Json.Serialization;

namespace CSharpInterviewMessageProcessor.Helpers.WebRequests;

public class VinQueryResult
{
    [JsonPropertyName("Count")]
    public long Count { get; set; }

    [JsonPropertyName("Message")]
    public string Message { get; set; }

    [JsonPropertyName("SearchCriteria")]
    public string SearchCriteria { get; set; }

    [JsonPropertyName("Results")]
    public QueryVariables[] Results { get; set; }
}

public class QueryVariables
{
    [JsonPropertyName("Value")]
    public string Value { get; set; }

    [JsonPropertyName("ValueId")]
    public string ValueId { get; set; }

    [JsonPropertyName("Variable")]
    public string Variable { get; set; }

    [JsonPropertyName("VariableId")]
    public long VariableId { get; set; }
}
