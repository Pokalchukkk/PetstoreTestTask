using System.Text.Json.Serialization;

namespace PetstoreTestTask.Models;

public sealed class Pet
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("category")]
    public Category? Category { get; init; }

    [JsonPropertyName("photoUrls")]
    public List<string> PhotoUrls { get; init; } = [];

    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; init; } = [];

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
}
