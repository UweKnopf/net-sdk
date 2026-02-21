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
    public string GetImageUrl(Quality quality, Extension extension)
    {
        return $"{this.Image}/{quality}.{extension}";
    }

    public byte[]? GetImage(Quality quality, Extension extension)
    {
        return this.TCGDex.GetImage(GetImageUrl(quality, extension));
    }

    public async Task<Set> GetSet()
    {
        return await this.Set.GetFullSet();
    }

    public async Task<Serie> GetSerie()
    {
        var fullSet = await this.Set.GetFullSet();
        return await fullSet.Serie.GetFullSerie();
    }
}
