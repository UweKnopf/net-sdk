using net_sdk.src.internal_classes;
using net_sdk.src.models.subs;

namespace net_sdk.src.models;

public record class Card(
    string Id,
    string LocalId,
    string Name,
    string? Image,
    string? Illustrator,
    string Rarity,
    string Category,
    CardVariants? Variants,
    SetResume Set,
    Pricing? Pricing,
    List<int>? DexId,
    int? Hp,
    List<string>? Types,
    string? EvolveFrom,
    string? Description,
    string? Level,
    string? Stage,
    string? Suffix,
    CardItem? Item,
    List<CardAbility> Abilities,
    List<CardAttack> Attacks,
    List<CardWeakRes> Weaknesses,
    List<CardWeakRes> Resistances,
    int? Retreat,
    string? Effect,
    string? TrainerType,
    string? EnergyType,
    string? RegulationMark,
    Legal Legal
    ) : Model()
{
    /// <summary>
    /// Returns the URL (relative to the API base URL) of the card image with the specified quality and extension.
    /// </summary>
    /// <param name="quality"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    public string? GetImageUrl(Quality quality, Extension extension)
    {
        if (Image == null) return null;
        return $"{Image}/{quality}.{extension}";
    }
    /// <summary>
    /// Async returns the card image as a byte array with the specified quality and extension.
    /// </summary>
    /// <param name="quality"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    public async Task<byte[]?> GetImage(Quality quality, Extension extension)
    {
        var imageUrl = GetImageUrl(quality, extension);
        if (imageUrl == null) return null;
        return await TCGDex.GetImage(imageUrl);
    }
    /// <summary>
    /// Async returns the full <see cref="Set"/> of the Card.
    /// </summary>
    /// <returns></returns>
    public async Task<Set?> GetFullSet()
    {
        if (Set == null)
            return null;
        return await Set.GetFullSet();
    }
    /// <summary>
    /// Async returns the full <see cref="Serie"/> of the Card.
    /// </summary>
    /// <returns></returns>
    public async Task<Serie?> GetFullSerie()
    {
        var set = await GetFullSet(); 
        if (set == null)
            return null;
        return await set.GetFullSerie();
    }
}
