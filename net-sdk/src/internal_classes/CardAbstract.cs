using System;

namespace net_sdk.src.internal_classes;

public abstract record class CardAbstract(
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
    public string GetImageUrl(Quality quality, Extension extension)
    {
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
        return await TCGDex.GetImage(GetImageUrl(quality, extension));
    }
}
