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

) : Model()
{
    public string? GetLogoUrl(Extension extension)
    {
        if (Logo == null) return null;
        return $"{Logo}.{extension}";
    }

    public async Task<byte[]?> GetLogo(Extension extension)
    {
        var logoUrl = GetLogoUrl(extension);
        if (logoUrl == null) return null;
        return await TCGDex.GetImage(logoUrl);
        
    }

    public string? GetSymbolUrl(Extension extension)
    {
        if (Symbol == null) return null;
        return $"{Symbol}.{extension}";
    }

    public async Task<byte[]?> GetSymbol(Extension extension)
    {
        var symbolUrl = GetSymbolUrl(extension);
        if (symbolUrl == null) return null;
        return await TCGDex.GetImage(symbolUrl);
        
    }
    //Both of these should potentially return full objects because the resume is already a property

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
