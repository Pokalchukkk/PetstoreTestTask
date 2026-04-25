using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PetstoreTestTask.Configuration;
using PetstoreTestTask.Models;

namespace PetstoreTestTask.Api;

public sealed class PetApiClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public PetApiClient() : this(ApiConfiguration.Default) { }

    public PetApiClient(ApiConfiguration config)
    {
        _http = new HttpClient { BaseAddress = new Uri(config.BaseUrl) };
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<ApiResponse<Pet>> CreatePetAsync(Pet pet)
    {
        var content = Serialize(pet);
        var response = await _http.PostAsync("/v2/pet", content);
        return await ParseAsync<Pet>(response);
    }

    public async Task<ApiResponse<Pet>> GetPetAsync(long petId)
    {
        var response = await _http.GetAsync($"/v2/pet/{petId}");
        return await ParseAsync<Pet>(response);
    }

    public async Task<ApiResponse<Pet>> DeletePetAsync(long petId)
    {
        var response = await _http.DeleteAsync($"/v2/pet/{petId}");
        return await ParseAsync<Pet>(response);
    }

private static StringContent Serialize<T>(T body)
    {
        var json = JsonSerializer.Serialize(body, JsonOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private static async Task<ApiResponse<T>> ParseAsync<T>(HttpResponseMessage response)
    {
        var raw = await response.Content.ReadAsStringAsync();

        T? body = default;
        if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(raw))
            body = JsonSerializer.Deserialize<T>(raw, JsonOptions);

        return new ApiResponse<T>(response.StatusCode, body, raw);
    }
}
