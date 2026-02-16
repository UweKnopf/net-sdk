using net_sdk.src.internal_classes;
using RestSharp;

namespace net_sdk.src.models;

public record class CardResume(
    string id,
    string localId,
    string name,
    string? image
) :Model() {
    public string getImageUrl(Quality quality, Extension extension)
    {
        return $"{this.image}/{quality}.{extension}";
    }

    public byte[]? getImage(Quality quality, Extension extension)
    {
        //probably wasteful to start another client here but no idea how to use the maine one in TCGDex without exposing the _client to the user
        RestClient restClient = new RestClient();
        restClient.AddDefaultHeader("user-agent", "@UweKnopf/net-sdk");
        var fileBytes = restClient.DownloadData(new RestRequest(getImageUrl(quality, extension), Method.Get));
        return fileBytes;
    }

    public async Task<Card?> getFullCard()
    {
        //tCGDex = new TCGDex("en");
        return await this.tCGDex.fetchCard(id);
    }
}
