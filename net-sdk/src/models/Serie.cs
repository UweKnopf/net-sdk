using net_sdk.src.internal_classes;

namespace net_sdk.src.models;

public record class Serie(
    List<SetResume> sets,
    string id,
    string name,
    string? logo
    
) : Model()
{
    public string? GetLogoUrl(Extension extension)
    {
        if (logo == null) return null;
        return $"{this.logo}.{extension}";
    }

    public byte[]? GetLogo(Extension extension)
    {
        var logoUrl = GetLogoUrl(extension);
        if (logoUrl == null) return null;
        return TCGDex.GetImage(logoUrl);
        
    }
}
