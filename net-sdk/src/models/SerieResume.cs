using net_sdk.src.internal_classes;

namespace net_sdk.src.models;

public abstract record class SerieResume(
    string id,
    string name,
    string? logo

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

    public async Task<Serie?> getFullSerie()
    {
        return await this.tCGDex.fetchSeries(this.id);
    }
}
