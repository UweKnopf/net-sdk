using Microsoft.VisualStudio.TestTools.UnitTesting;
using net_sdk.src;
using net_sdk.src.models;

namespace net_sdkTest.MainTests;

[TestClass]
public class SerieTest
{
    private async Task<Serie> GetTestSerieEN()
    {
        var sdk = new TCGDex("en");
        return await sdk.FetchSerie("swsh");
    }
    [TestMethod]
    public async Task GetLogoUrl_LogoUrlExistsForPng_LogoUrlString()
    {
        var Serie = await GetTestSerieEN();
        
        Assert.IsNotNull(Serie.GetLogoUrl(Extension.png));
    }

    [TestMethod]
    public async Task GetLogo_LogoExistsForPng_LogoAsBytes()
    {
        var Serie = await GetTestSerieEN();
        
        Assert.IsNotNull(Serie.GetLogo(Extension.png));
    }
}
