namespace net_sdk.src;

/// <summary>
/// The quality of the image to be fetched. High quality images are 600x825 pixels, low quality images are 245x337 pixels.
/// </summary>
public enum Quality
{
    //needs to be lowercase because custom enum string values are not possible
    high,
    low,
}
