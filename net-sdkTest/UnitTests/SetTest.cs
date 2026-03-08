using Microsoft.VisualStudio.TestTools.UnitTesting;
using net_sdk.src;
using net_sdk.src.models;

namespace net_sdkTest.UnitTests;
[TestClass]
public class SetTest
{

    private static async Task<Set> GetTestSetEN()
    {
        var sdk = new TCGDex("en");
        return await sdk.FetchSet("swsh3");
    }

    private static async Task<SetResume> GetTestSetResumeEN()
    {
        var sdk = new TCGDex("en");
        var setResumeList = await sdk.FetchSets(new Query().Equal("id", "swsh3"));
        return setResumeList[0];
    }
    [TestMethod]
    public async Task GetLogoUrl_LogoUrlExistsForPng_LogoUrlString()
    {
        var set = await GetTestSetEN();
        
        Assert.IsNotNull(set.GetLogoUrl(Extension.png));
    }

    [TestMethod]
    public async Task GetLogo_LogoExistsForPng_LogoAsBytes()
    {
        var set = await GetTestSetEN();
        
        Assert.IsNotNull(set.GetLogo(Extension.png));
    }

    [TestMethod]
    public async Task GetSymbolUrl_SymbolUrlExistsForPng_SymbolUrlString()
    {
        var set = await GetTestSetEN();
        
        Assert.IsNotNull(set.GetSymbolUrl(Extension.png));
    }

    [TestMethod]
    public async Task GetSymbol_SymbolExistsForPng_SymbolAsBytes()
    {
        var set = await GetTestSetEN();
        
        Assert.IsNotNull(set.GetSymbol(Extension.png));
    }

    [TestMethod]
    public async Task GetSerie_SerieExists_SerieObjectOfCard()
    {
        var set = await GetTestSetEN();

        var serie = await set.GetFullSerie();
        

        Assert.AreEqual("swsh", serie!.Id);
    }


    [TestMethod]
    public async Task UseFieldSerie_SerieFieldExists_FullSerie()
    {
        var set = await GetTestSetEN();

        var serieResume = set.Serie;

        var serie = await serieResume.GetFullSerie();
        

        Assert.IsNotNull(serie);
    }

    [TestMethod]
    public async Task UseFieldCards_CardsFieldExists_FullCard()
    {
        var set = await GetTestSetEN();

        var cardResumes = set.Cards;

        var card = await cardResumes[0].GetFullCard();
        

        Assert.IsNotNull(card);
    }

    [TestMethod]
    public async Task UseFieldSetCardCount_CardCountFieldExists_FullCardCountObject()
    {
        var set = await GetTestSetEN();

        var setCardCount = set.CardCount;
        

        Assert.IsNotNull(setCardCount);
        Assert.IsNotNull(setCardCount.FirstEd);
        Assert.IsNotNull(setCardCount.Holo);
        Assert.IsNotNull(setCardCount.Normal);
        Assert.IsNotNull(setCardCount.Reverse);
        Assert.IsNotNull(setCardCount.Total);
        Console.WriteLine(setCardCount);
    }

    [TestMethod]
    public async Task UseFieldSetResumeCardCountResume_CardCountResumeFieldExists_FullCardCountResumeObject()
    {
        var set = await GetTestSetResumeEN();

        var setCardCountResume = set.CardCount;
        
        Console.WriteLine(setCardCountResume);
        Assert.IsNotNull(setCardCountResume);
        Assert.IsNotNull(setCardCountResume.Total);
        
    }
}
