using net_sdk.src.internal_classes;
using net_sdk.src.models.subs;

namespace net_sdk.src.models;

public record class Card(
    string id,
    string localId,
    string name,
    string? image,
    string? illustrator,
    string rarity,
    string category,
    CardVariants? varients,
    SetResume set,
    Pricing? pricing,
    List<int>? dexId,
    int? hp,
    List<string>? types,
    string? evolveFrom,
    string? description,
    string? level,
    string? stage,
    string? suffix,
    CardItem? item,
    List<CardAbility> abilities,
    List<CardAttack> attacks,
    List<CardWeakRes> weaknesses,
    List<CardWeakRes> resistances,
    int? retreat,
    string? effect,
    string? trainerType,
    string? energyType,
    string? regulationMark,
    Legal legal
    ):Model()
{
    public string getImageUrl(Quality quality, Extension extension)
    {
        return $"{this.image}/{quality}.{extension}";
    }

    public byte[]? getImage(Quality quality, Extension extension)
    {
        return this.tCGDex.getImage(getImageUrl(quality, extension));
    }
}
