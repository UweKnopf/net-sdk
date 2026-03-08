using Microsoft.VisualStudio.TestTools.UnitTesting;
using net_sdk.src;
using net_sdk.src.models;

namespace net_sdkTest.MainTests;

[TestClass]
public class CardTest
{

    private async Task<Card> GetTestCardEN()
    {
        var sdk = new TCGDex("en");
        return await sdk.FetchCard("swsh3-136");
    }

    private async Task<Card> GetWrongTestCardEN()
    {
        var sdk = new TCGDex("en");
        return await sdk.FetchCard("BADCARDID");
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
        

        Assert.IsNotNull(await card.GetImage(Quality.low, Extension.png));
    }

    [TestMethod]
    public async Task GetSet_SetExists_SetObjectOfCard()
    {
        var card = await GetTestCardEN();

        var set = await card.GetFullSet();
        

        Assert.AreEqual("swsh3", set.Id);
    }

    [TestMethod]
    public async Task GetSerie_SerieExists_SerieObjectOfCard()
    {
        var card = await GetTestCardEN();

        var serie = await card.GetFullSerie();
        

        Assert.AreEqual("swsh", serie.Id);
    }

    [TestMethod]
    public async Task UseFieldSet_SetFieldExists_FullSerie()
    {
        var card = await GetTestCardEN();

        var setResume = card.Set;

        var fullSet = await setResume.GetFullSet();
        

        Assert.IsNotNull(fullSet);
    }

    [TestMethod]
    public async Task GetImage_CardDoesntExist_ImageIsNull()
    {
        var card = await GetWrongTestCardEN();
        
        var image = await card.GetImage(Quality.low, Extension.png);
        Assert.IsNull(image);
    }

    [TestMethod]
    public async Task GetSerie_CardAndSerieDosntExist_SerieIsNull()
    {
        var card = await GetWrongTestCardEN();

        var serie = await card.GetFullSerie();
        

        Assert.IsNull(serie);
    }
}
