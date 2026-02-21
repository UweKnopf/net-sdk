using net_sdk.src.internal_classes;
using net_sdk.src.models.subs;

namespace net_sdk.src.models;

public record class SetResume(
    string Id,
    string? Name,
    string? Logo,
    string? Symbol,
    SetCardCount CardCount
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

    public async Task<Set?> GetFullSet()
    {
        return await TCGDex.FetchSet(Id);
    }
}
