using PetstoreTestTask.Models;

namespace PetstoreTestTask.Builders;

public static class PetBuilder
{
    public static Pet Minimal(string name, string status = "available") => new()
    {
        Id = GenerateUniqueId(),
        Name = name,
        PhotoUrls = ["https://example.com/photo.jpg"],
        Status = status
    };

    public static Pet Full(string name, string categoryName, string status, params string[] tagNames) => new()
    {
        Id = GenerateUniqueId(),
        Name = name,
        Category = new Category { Id = 1, Name = categoryName },
        PhotoUrls = ["https://example.com/photo.jpg"],
        Tags = tagNames.Select((tagName, index) => new Tag { Id = index + 1, Name = tagName }).ToList(),
        Status = status
    };

    public static Pet WithPhotoUrls(string name, string status, string photoUrl1, string photoUrl2) => new()
    {
        Id = GenerateUniqueId(),
        Name = name,
        PhotoUrls = [photoUrl1, photoUrl2],
        Status = status
    };

    public static Pet WithZeroId(string name, string status = "available") => new()
    {
        Id = 0,
        Name = name,
        PhotoUrls = ["https://example.com/photo.jpg"],
        Status = status
    };

    private static long _nextId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 900_000_000L;
    private static long GenerateUniqueId() => Interlocked.Increment(ref _nextId);
}
