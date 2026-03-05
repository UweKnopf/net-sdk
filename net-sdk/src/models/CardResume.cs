using net_sdk.src.internal_classes;
using RestSharp;

namespace net_sdk.src.models;

public record class CardResume(
    string Id,
    string LocalId,
    string Name,
    string? Image
) : CardAbstract(Id, LocalId, Name, Image)
{
    /// <summary>
    /// Async returns a <see cref="Card"/> of the CardResume.
    /// </summary>
    /// <returns></returns>
    public async Task<Card?> GetFullCard()
    {
        return await TCGDex.FetchCard(Id);
    }
}
