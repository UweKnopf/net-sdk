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
    /// <summary>
    /// Returns the URL of the card image with the specified quality and extension.
    /// </summary>
    /// <param name="quality"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    public string? GetImageUrl(Quality quality, Extension extension)
    {
        if (Image == null) return null;
        return $"{Image}/{quality}.{extension}";
    }
    /// <summary>
    /// Async returns the card image as a byte array with the specified quality and extension.
    /// </summary>
    /// <param name="quality"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    public async Task<byte[]?> GetImage(Quality quality, Extension extension)
    {
        var imageUrl = GetImageUrl(quality, extension);
        if (imageUrl == null) return null;
        return await TCGDex.GetImage(imageUrl);
    }
    /// <summary>
    /// Async returns a <see cref="Card"/> of the CardResume.
    /// </summary>
    /// <returns></returns>
    public async Task<Card> GetFullCard()
    {
        return await TCGDex.FetchCard(Id);
    }
}
