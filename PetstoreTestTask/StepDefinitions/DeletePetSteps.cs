using PetstoreTestTask.Api;
using PetstoreTestTask.Contexts;
using Reqnroll;

namespace PetstoreTestTask.StepDefinitions;

[Binding]
public sealed class DeletePetSteps(PetScenarioContext ctx, PetApiClient apiClient)
{
    [When("I send a DELETE request for that pet")]
    public async Task WhenIDeleteThatPetAsync()
    {
        var response = await apiClient.DeletePetAsync(ctx.ActivePetId!.Value);
        ctx.LastStatusCode = response.StatusCode;
        ctx.LastReceivedPet = null;
    }

    [When("I send a DELETE request for pet with id {long}")]
    public async Task WhenIDeletePetByIdAsync(long petId)
    {
        var response = await apiClient.DeletePetAsync(petId);
        ctx.LastStatusCode = response.StatusCode;
    }

    [When("I send a DELETE request for the pet labeled {string}")]
    public async Task WhenIDeletePetByLabelAsync(string label)
    {
        var response = await apiClient.DeletePetAsync(ctx.LabeledPetIds[label]);
        ctx.LastStatusCode = response.StatusCode;
    }
}
