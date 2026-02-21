using net_sdk.src.internal_classes;

namespace net_sdk.src.models;

public record class Serie(
    List<SetResume> Sets,
    string Id,
    string Name,
    string? Logo
    
) : Model()
{
    public string? GetLogoUrl(Extension extension)
    {
        if (Logo == null) return null;
        return $"{this.Logo}.{extension}";
    }

    public byte[]? GetLogo(Extension extension)
    {
        var logoUrl = GetLogoUrl(extension);
        if (logoUrl == null) return null;
        return TCGDex.GetImage(logoUrl);
        
    }
}
