namespace PetstoreTestTask.Configuration;

public sealed class ApiConfiguration
{
    public string BaseUrl { get; init; } = "https://petstore.swagger.io";

    public static ApiConfiguration Default { get; } = new();
}
