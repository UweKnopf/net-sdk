using net_sdk.src.internal_classes;

namespace net_sdk.src.models;

public record class SerieResume(
    string Id,
    string Name,
    string? Logo

) : SerieAbstract(Id, Name, Logo)
{
    /// <summary>
    /// Async returns a <see cref="Serie"/> of the SerieResume.
    /// </summary>
    /// <returns></returns>
    public async Task<Serie?> GetFullSerie()
    {
        return await TCGDex.FetchSerie(Id);
    }
}
