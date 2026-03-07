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
    /// <summary>
    /// Async returns the logo url of the set, based on the <see cref="Logo"/> property and the given <paramref name="extension"/>.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns>Returns the logo url of the set as a string.</returns>
    public string? GetLogoUrl(Extension extension)
    {
        if (Logo == null) return null;
        return $"{Logo}.{extension}";
    }
    /// <summary>
    /// Async returns the logo of the set, based on the <see cref="Logo"/> property and the given <paramref name="extension"/>.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns>Returns the logo of the set as a byte array.</returns>
    public async Task<byte[]?> GetLogo(Extension extension)
    {
        var logoUrl = GetLogoUrl(extension);
        if (logoUrl == null) return null;
        return await TCGDex.GetImage(logoUrl);
        
    }
    /// <summary>
    /// Async returns the symbol url of the set, based on the <see cref="Symbol"/> property and the given <paramref name="extension"/>.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns>Returns the symbol url of the set as a string.</returns>
    public string? GetSymbolUrl(Extension extension)
    {
        if (Symbol == null) return null;
        return $"{Symbol}.{extension}";
    }
    /// <summary>
    /// Async returns the symbol of the set, based on the <see cref="Symbol"/> property and the given <paramref name="extension"/>.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns>Returns the symbol of the set as a byte array.</returns>
    public async Task<byte[]?> GetSymbol(Extension extension)
    {
        var symbolUrl = GetSymbolUrl(extension);
        if (symbolUrl == null) return null;
        return await TCGDex.GetImage(symbolUrl);
        
    }
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
