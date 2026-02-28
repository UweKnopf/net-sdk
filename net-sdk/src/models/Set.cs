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

    public byte[]? GetLogo(Extension extension)
    {
        var logoUrl = GetLogoUrl(extension);
        if (logoUrl == null) return null;
        return TCGDex.GetImage(logoUrl);
        
    }

    public string? GetSymbolUrl(Extension extension)
    {
        if (Symbol == null) return null;
        return $"{Symbol}.{extension}";
    }

    public byte[]? GetSymbol(Extension extension)
    {
        var symbolUrl = GetSymbolUrl(extension);
        if (symbolUrl == null) return null;
        return TCGDex.GetImage(symbolUrl);
        
    }

    public async Task<Serie> GetSerie()
    {
        var serieId = Serie.Id;
        return await TCGDex.FetchSerie(serieId);
    }
}
