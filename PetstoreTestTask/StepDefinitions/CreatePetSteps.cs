using FluentAssertions;
using PetstoreTestTask.Api;
using PetstoreTestTask.Builders;
using PetstoreTestTask.Contexts;
using Reqnroll;

namespace PetstoreTestTask.StepDefinitions;

[Binding]
public sealed class CreatePetSteps(PetScenarioContext ctx, PetApiClient apiClient)
{
    [Given("a minimal pet payload with name {string} and status {string}")]
    public void GivenMinimalPetPayload(string name, string status)
    {
        ctx.SentPet = PetBuilder.Minimal(name, status);
    }

    [Given("a full pet payload with name {string}, category {string}, status {string}, and tags {string} and {string}")]
    public void GivenFullPetPayload(string name, string category, string status, string tag1, string tag2)
    {
        ctx.SentPet = PetBuilder.Full(name, category, status, tag1, tag2);
    }

    [Given("a pet payload with name {string}, status {string}, and photo urls {string} and {string}")]
    public void GivenPetPayloadWithPhotoUrls(string name, string status, string photoUrl1, string photoUrl2)
    {
        ctx.SentPet = PetBuilder.WithPhotoUrls(name, status, photoUrl1, photoUrl2);
    }

    [Given("a pet payload with a zero id, name {string}, and status {string}")]
    public void GivenPetPayloadWithZeroId(string name, string status)
    {
        ctx.SentPet = PetBuilder.WithZeroId(name, status);
    }

    [When("I send a POST request to create the pet")]
    public async Task WhenICreateThePetAsync()
    {
        var response = await apiClient.CreatePetAsync(ctx.SentPet!);
        ctx.LastStatusCode = response.StatusCode;
        ctx.LastReceivedPet = response.Body;
        ctx.ActivePetId = response.Body?.Id;
    }

    [When("I save the returned pet id as {string}")]
    public void WhenISaveReturnedPetIdAs(string label)
    {
        ctx.LastReceivedPet.Should().NotBeNull("cannot save ID: last response had no body");
        ctx.LabeledPetIds[label] = ctx.LastReceivedPet!.Id;
    }
}
