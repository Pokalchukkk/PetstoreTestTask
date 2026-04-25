using System.Text.Json;
using FluentAssertions;
using PetstoreTestTask.Contexts;
using Reqnroll;

namespace PetstoreTestTask.StepDefinitions;

[Binding]
public sealed class PetResponseAssertionSteps(PetScenarioContext ctx)
{
    [Then("the response status code should be {int}")]
    public void ThenStatusCodeShouldBe(int expectedCode)
    {
        ((int)ctx.LastStatusCode).Should().Be(expectedCode,
            $"expected HTTP {expectedCode} but got {(int)ctx.LastStatusCode}");
    }

    [Then("the response should contain a pet with name {string}")]
    public void ThenResponseShouldContainPetWithName(string expectedName)
    {
        ctx.LastReceivedPet.Should().NotBeNull("response body was empty or failed to deserialize");
        ctx.LastReceivedPet!.Name.Should().Be(expectedName);
    }

    [Then("the response should contain pet status {string}")]
    public void ThenResponseShouldContainPetStatus(string expectedStatus)
    {
        ctx.LastReceivedPet.Should().NotBeNull("response body was empty or failed to deserialize");
        ctx.LastReceivedPet!.Status.Should().Be(expectedStatus);
    }

    [Then("the response should contain a non-zero pet id")]
    public void ThenResponseShouldContainNonZeroId()
    {
        ctx.LastReceivedPet.Should().NotBeNull();
        ctx.LastReceivedPet!.Id.Should().BeGreaterThan(0, "the API must assign a non-zero ID to a created pet");
    }

    [Then("the response should contain category name {string}")]
    public void ThenResponseShouldContainCategoryName(string expectedCategoryName)
    {
        ctx.LastReceivedPet.Should().NotBeNull();
        ctx.LastReceivedPet!.Category.Should().NotBeNull("category was part of the request payload");
        ctx.LastReceivedPet!.Category!.Name.Should().Be(expectedCategoryName);
    }

    [Then("the response should contain tag {string}")]
    public void ThenResponseShouldContainTag(string expectedTagName)
    {
        ctx.LastReceivedPet.Should().NotBeNull();
        ctx.LastReceivedPet!.Tags
            .Should().Contain(t => t.Name == expectedTagName,
                $"tag '{expectedTagName}' was included in the request payload but is missing from the response");
    }

[Then("the response should contain photo url {string}")]
    public void ThenResponseShouldContainPhotoUrl(string expectedUrl)
    {
        ctx.LastReceivedPet.Should().NotBeNull("response body was empty or failed to deserialize");
        ctx.LastReceivedPet!.PhotoUrls.Should().Contain(expectedUrl,
            $"photo URL '{expectedUrl}' was included in the request but is missing from the response");
    }

    [Then("the response body should contain field {string}")]
    public void ThenResponseBodyShouldContainField(string fieldName)
    {
        ctx.LastRawContent.Should().NotBeNullOrWhiteSpace("response body must not be empty to check for fields");
        using var doc = JsonDocument.Parse(ctx.LastRawContent);
        doc.RootElement.TryGetProperty(fieldName, out _).Should().BeTrue(
            $"expected field '{fieldName}' in response body: {ctx.LastRawContent}");
    }

    [Then("the ids saved as {string} and {string} should be different")]
    public void ThenSavedIdsShouldBeDifferent(string label1, string label2)
    {
        ctx.LabeledPetIds.Should().ContainKey(label1);
        ctx.LabeledPetIds.Should().ContainKey(label2);
        ctx.LabeledPetIds[label1].Should().NotBe(ctx.LabeledPetIds[label2],
            $"IDs saved as '{label1}' and '{label2}' should be different but both equal {ctx.LabeledPetIds[label1]}");
    }
}
