namespace net_sdk.src;

/// <summary>
/// The extension of the image to be fetched. Webp is highly recommended, especially if you want to display the images in a web application.
/// PNG should be used if you need the transparent background.
/// </summary>
public enum Extension
{
    //needs to be lowercase because custom enum string values are not possible
    png,
    jpg,
    webp,
}
