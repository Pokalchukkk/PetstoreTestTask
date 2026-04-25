using FluentAssertions;
using PetstoreTestTask.Api;
using PetstoreTestTask.Builders;
using PetstoreTestTask.Contexts;
using Reqnroll;
using System.Net;

namespace PetstoreTestTask.StepDefinitions;

[Binding]
public sealed class PetSetupSteps(PetScenarioContext ctx, PetApiClient apiClient)
{
    [Given("a pet with name {string} and status {string} exists in the store")]
    public async Task GivenAnExistingPetAsync(string name, string status)
    {
        var pet = PetBuilder.Minimal(name, status);
        var response = await apiClient.CreatePetAsync(pet);

        response.StatusCode.Should().Be(HttpStatusCode.OK,
            "test setup failed: POST /pet must succeed before running this scenario");

        ctx.SentPet = response.Body;
        ctx.ActivePetId = response.Body!.Id;
    }

    [Given("a pet labeled {string} with name {string} and status {string} exists in the store")]
    public async Task GivenALabeledExistingPetAsync(string label, string name, string status)
    {
        var pet = PetBuilder.Minimal(name, status);
        var response = await apiClient.CreatePetAsync(pet);

        response.StatusCode.Should().Be(HttpStatusCode.OK,
            "test setup failed: POST /pet must succeed before running this scenario");

        ctx.LabeledPetIds[label] = response.Body!.Id;
    }
}
