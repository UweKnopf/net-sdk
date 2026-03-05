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
    public byte[]? GetLogo(Extension extension)
    {
        var logoUrl = GetLogoUrl(extension);
        if (logoUrl == null) return null;
        return TCGDex.GetImage(logoUrl);
        
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
    public byte[]? GetSymbol(Extension extension)
    {
        var symbolUrl = GetSymbolUrl(extension);
        if (symbolUrl == null) return null;
        return TCGDex.GetImage(symbolUrl);
        
    }
    /// <summary>
    /// Async returns a <see cref="Set"/> of the SetResume.
    /// </summary>
    /// <returns>Returns the full set as a <see cref="Set"/>.</returns>
    public async Task<Set?> GetFullSet()
    {
        return await TCGDex.FetchSet(Id);
    }
}
