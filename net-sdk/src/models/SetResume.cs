using net_sdk.src.internal_classes;
using net_sdk.src.models.subs;

namespace net_sdk.src.models;

public record class SetResume(
    string Id,
    string? Name,
    string? Logo,
    string? Symbol,
    SetCardCount CardCount
) : SetAbstract(Id, Name, Logo, Symbol, CardCount)
{
    /// <summary>
    /// Async returns a <see cref="Set"/> of the SetResume.
    /// </summary>
    /// <returns>Returns the full set as a <see cref="Set"/>.</returns>
    public async Task<Set?> GetFullSet()
    {
        return await TCGDex.FetchSet(Id);
    }
}
