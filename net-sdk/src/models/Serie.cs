using net_sdk.src.internal_classes;

namespace net_sdk.src.models;

public record class Serie(
    List<SetResume> Sets,
    string Id,
    string Name,
    string? Logo
    
) : Model()
{
    /// <summary>
    /// Returns the URL of the logo of the serie. If the serie has no logo, null is returned.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public string? GetLogoUrl(Extension extension)
    {
        if (Logo == null) return null;
        return $"{Logo}.{extension}";
    }
    /// <summary>
    /// Async returns the logo of the serie as a byte array. If the serie has no logo, null is returned.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public async Task<byte[]?> GetLogo(Extension extension)
    {
        var logoUrl = GetLogoUrl(extension);
        if (logoUrl == null) return null;
        return await TCGDex.GetImage(logoUrl);
        
    }
}
