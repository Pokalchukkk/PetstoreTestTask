using System.Text.Json.Serialization;

namespace PetstoreTestTask.Models;

public sealed class Tag
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
