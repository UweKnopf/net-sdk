using Microsoft.VisualStudio.TestTools.UnitTesting;
using net_sdk.src;
using net_sdk.src.models;

namespace net_sdkTest.MainTests;
[TestClass]
public class SetTest
{

    private async Task<Set> GetTestSetEN()
    {
        var sdk = new TCGDex("en");
        return await sdk.FetchSet("swsh3");
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

        var serie = await set.GetSerie();
        

        Assert.AreEqual("swsh", serie.Id);
    }
}
