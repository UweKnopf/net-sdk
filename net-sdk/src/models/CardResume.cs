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

    public byte[]? GetImage(Quality quality, Extension extension)
    {
        //probably wasteful to start another client here but no idea how to use the maine one in TCGDex without exposing the _client to the user
        RestClient restClient = new RestClient();
        restClient.AddDefaultHeader("user-agent", "@UweKnopf/net-sdk");
        var fileBytes = restClient.DownloadData(new RestRequest(GetImageUrl(quality, extension), Method.Get));
        return fileBytes;
    }

    public async Task<Card?> GetFullCard()
    {
        //tCGDex = new TCGDex("en");
        return await this.TCGDex.FetchCard(Id);
    }
}
