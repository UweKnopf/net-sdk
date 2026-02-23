using Microsoft.VisualStudio.TestTools.UnitTesting;
using net_sdk.src;
using net_sdk.src.models;

namespace net_sdkTest.MainTests.TCGDexClass;

[TestClass]
public class CardTest
{

    private async Task<Card> GetTestCardEN()
    {
        var sdk = new TCGDex("en");
        return await sdk.FetchCard("swsh3-136");
    }

    [TestMethod]
    public async Task GetImageUrl_ImageUrlExistsForLowAndPng_ImageUrlString()
    {
        var card = await GetTestCardEN();
        

        Assert.AreEqual("https://assets.tcgdex.net/en/swsh/swsh3/136/low.png", card.GetImageUrl(Quality.low, Extension.png));
    }

    [TestMethod]
    public async Task GetImage_ImageExistsForLowAndPng_ImageAsBytes()
    {
        var card = await GetTestCardEN();
        

        Assert.IsNotNull(card.GetImage(Quality.low, Extension.png));
    }

    [TestMethod]
    public async Task GetSet_SetExists_SetObjectOfCard()
    {
        var card = await GetTestCardEN();

        var set = await card.GetSet();
        

        Assert.AreEqual("swsh3", set.Id);
    }

    [TestMethod]
    public async Task GetSerie_SerieExists_SerieObjectOfCard()
    {
        var card = await GetTestCardEN();

        var serie = await card.GetSerie();
        

        Assert.AreEqual("swsh", serie.Id);
    }
}
