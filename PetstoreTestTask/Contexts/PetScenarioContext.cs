using System.Net;
using PetstoreTestTask.Models;

namespace PetstoreTestTask.Contexts;

public sealed class PetScenarioContext
{
    public Pet? SentPet { get; set; }

    public Pet? LastReceivedPet { get; set; }

    public HttpStatusCode LastStatusCode { get; set; }

    public long? ActivePetId { get; set; }

    public string LastRawContent { get; set; } = string.Empty;

    public Dictionary<string, long> LabeledPetIds { get; } = new();
}
