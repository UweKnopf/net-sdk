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

    [TestMethod]
    public async Task UsePropertySets_SetsExist_AFullSet()
    {
        var Serie = await GetTestSerieEN();

        var sets = Serie.Sets;

        var set = sets[0].GetFullSet();
        
        Assert.IsNotNull(set);
    }

    [TestMethod]
    public async Task GetCards_SetsExistAndHasSeriesWithCards_AListOfAllCardResumesInTheSerie()
    {
        var Serie = await GetTestSerieEN();

        var cards = await Serie.GetCards();

        Assert.IsNotNull(cards);
    }

    [TestMethod]
    public async Task GetTotalCardCount_SetsExistAndHasSeriesWithCards_TotalCardCount()
    {
        var Serie = await GetTestSerieEN();

        var totalCardCount = Serie.GetTotalCardCount();

        Assert.IsNotNull(totalCardCount);
        Assert.IsGreaterThan((int)totalCardCount, 4000);
    }
}
