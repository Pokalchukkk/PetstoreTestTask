using PetstoreTestTask.Api;
using PetstoreTestTask.Contexts;
using Reqnroll;

namespace PetstoreTestTask.StepDefinitions;

[Binding]
public sealed class GetPetSteps(PetScenarioContext ctx, PetApiClient apiClient)
{
    [When("I send a GET request for that pet")]
    public async Task WhenIGetThatPetAsync()
    {
        var response = await apiClient.GetPetAsync(ctx.ActivePetId!.Value);
        ctx.LastStatusCode = response.StatusCode;
        ctx.LastReceivedPet = response.Body;
        ctx.LastRawContent = response.RawContent;
    }

    [When("I send a GET request for pet with id {long}")]
    public async Task WhenIGetPetByIdAsync(long petId)
    {
        var response = await apiClient.GetPetAsync(petId);
        ctx.LastStatusCode = response.StatusCode;
        ctx.LastReceivedPet = response.Body;
        ctx.LastRawContent = response.RawContent;
    }

[When("I send a GET request for the pet labeled {string}")]
    public async Task WhenIGetPetByLabelAsync(string label)
    {
        var response = await apiClient.GetPetAsync(ctx.LabeledPetIds[label]);
        ctx.LastStatusCode = response.StatusCode;
        ctx.LastReceivedPet = response.Body;
        ctx.LastRawContent = response.RawContent;
    }
}
