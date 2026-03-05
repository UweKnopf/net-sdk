using net_sdk.src.internal_classes;
using RestSharp;

namespace net_sdk.src.models;

public record class CardResume(
    string Id,
    string LocalId,
    string Name,
    string? Image
) : Model()
{
    public string GetImageUrl(Quality quality, Extension extension)
    {
        return $"{this.Image}/{quality}.{extension}";
    }

    public async Task<byte[]?> GetImage(Quality quality, Extension extension)
    {
        
        return await TCGDex.GetImage(GetImageUrl(quality, extension));
    }

    public async Task<Card?> GetFullCard()
    {
        //tCGDex = new TCGDex("en");
        return await this.TCGDex.FetchCard(Id);
    }
}
