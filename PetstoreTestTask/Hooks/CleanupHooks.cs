using PetstoreTestTask.Api;
using PetstoreTestTask.Contexts;
using Reqnroll;

namespace PetstoreTestTask.Hooks;

[Binding]
public sealed class CleanupHooks(PetScenarioContext ctx, PetApiClient apiClient)
{
    [AfterScenario]
    public async Task DeleteCreatedPetAsync()
    {
        var ids = ctx.LabeledPetIds.Values.ToHashSet();
        if (ctx.ActivePetId.HasValue)
            ids.Add(ctx.ActivePetId.Value);

        foreach (var id in ids)
        {
            try { await apiClient.DeletePetAsync(id); }
            catch { }
        }
    }
}
