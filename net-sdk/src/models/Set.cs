using net_sdk.src.internal_classes;
using net_sdk.src.models.subs;

namespace net_sdk.src.models;

public record class Set(
    string id,
    string? name,
    string? logo,
    string? symbol,
    SerieResume serie,
    string? tcgOnline,
    string? releaseDate,
    Legal legal,
    SetCardCount cardCount,
    List<CardResume> cards

) : Model()
{
    public string? getLogoUrl(Extension extension)
    {
        if (logo == null) return null;
        return $"{this.logo}.{extension}";
    }

    public byte[]? getLogo(Extension extension)
    {
        var logoUrl = getLogoUrl(extension);
        if (logoUrl == null) return null;
        return tCGDex.getImage(logoUrl);
        
    }

    public string? getSymbolUrl(Extension extension)
    {
        if (symbol == null) return null;
        return $"{this.symbol}.{extension}";
    }

    public byte[]? getSymbol(Extension extension)
    {
        var symbolUrl = getSymbolUrl(extension);
        if (symbolUrl == null) return null;
        return tCGDex.getImage(symbolUrl);
        
    }
}
