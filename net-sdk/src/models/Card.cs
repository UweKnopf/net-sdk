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
    ) : CardAbstract(Id, LocalId, Name, Image)
{
    /// <summary>
    /// Async returns the <see cref="Set"/> of the Card.
    /// </summary>
    /// <returns></returns>
    public async Task<Set> GetSet()
    {
        return await Set.GetFullSet();
    }
    /// <summary>
    /// Async returns the <see cref="Serie"/> of the Card.
    /// </summary>
    /// <returns></returns>
    public async Task<Serie> GetSerie()
    {
        var set = await GetSet(); 
        return await set.GetSerie();
    }
}
