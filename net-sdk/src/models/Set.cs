using net_sdk.src.internal_classes;
using net_sdk.src.models.subs;

namespace net_sdk.src.models;

public record class Set(
    string Id,
    string? Name,
    string? Logo,
    string? Symbol,
    SerieResume Serie,
    string? TcgOnline,
    string? ReleaseDate,
    Legal Legal,
    SetCardCount CardCount,
    List<CardResume> Cards

) : SetAbstract(Id, Name, Logo, Symbol, CardCount)
{
    public async Task<Serie> GetSerie()
    {
        return await Serie.GetFullSerie();
    }
    //what if caller wants to use the card field directly?
    public List<CardResume> GetCards()
    {
        return Cards;
    }
}
