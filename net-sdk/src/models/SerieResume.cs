using net_sdk.src.internal_classes;

namespace net_sdk.src.models;

public record class SerieResume(
    string Id,
    string Name,
    string? Logo

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

    public async Task<Serie?> GetFullSerie()
    {
        return await TCGDex.FetchSerie(Id);
    }
}
