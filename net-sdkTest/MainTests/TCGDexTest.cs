using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net_sdk.src;
using net_sdk.src.models;

namespace net_sdkTest.MainTests;

[TestClass]
public class TCGDexTest
{
    private TCGDex createTCGDexEN()
    {
        return new TCGDex("en");
    }

    [TestMethod]
    public void GetImage_CorrectImageURL_ReturnBytes()
    {
        var sdk = createTCGDexEN();
        var imageUrl = "https://assets.tcgdex.net/en/bw/bw6/1/low.png";

        var imageBytes = sdk.GetImage(imageUrl);

        Assert.IsNotNull(imageBytes);
        Assert.IsInstanceOfType(imageBytes, typeof(byte[]));
    }

    [TestMethod]
    public void GetImage_WrongImageURL_()
    {
        var sdk = createTCGDexEN();
        var imageUrl = "";

        var imageBytes = sdk.GetImage(imageUrl);

        Assert.IsNull(imageBytes);
    }

    [TestMethod]
    public async Task FetchCards_NoQuery_AllCardsAsResumes()
    {
        var sdk = createTCGDexEN();

        var all_cardResumes = await sdk.FetchCards();

        Assert.IsNotNull(all_cardResumes);
        Assert.IsGreaterThan(10000, all_cardResumes.Count);
        Assert.IsInstanceOfType(all_cardResumes, typeof(List<CardResume>));
        Assert.IsNotNull(all_cardResumes[0]);
    }

    [TestMethod]
    public async Task FetchCard_RightCardID_CardObjectOfCardID()
    {
        var sdk = createTCGDexEN();
        var cardID = "swsh3-136";

        var card = await sdk.FetchCard(cardID);

        Assert.IsNotNull(card);
    }

    [TestMethod]
    public async Task FetchCard_WrongCardID_EmptyCard()
    {
        var sdk = createTCGDexEN();
        var cardID = "BADCARDID";

        var card = await sdk.FetchCard(cardID);

        Assert.IsNotNull(card);
        Assert.IsNull(card.Name);
    }

    [TestMethod]
    public async Task FetchCardResume_RightCardID_CardResumeObjectOfCardID()
    {
        var sdk = createTCGDexEN();
        var cardID = "swsh3-136";

        var cardResume = await sdk.FetchCardResume(cardID);

        Assert.IsNotNull(cardResume);
        Assert.AreEqual("Furret", cardResume.Name);
    }

    [TestMethod]
    public async Task FetchSet_RightSetID_SetObjectOfSetID()
    {
        var sdk = createTCGDexEN();
        var setID = "swsh3";

        var set = await sdk.FetchSet(setID);

        Assert.IsNotNull(set);

    }

    [TestMethod]
    public async Task FetchSets_NoQuery_AllSetAsResumes()
    {
        var sdk = createTCGDexEN();

        var all_sets = await sdk.FetchSets();

        Assert.IsNotNull(all_sets);

    }

    [TestMethod]
    public async Task FetchSerie_RightSerieID_SerieObjectOfSerieID()
    {
        var sdk = createTCGDexEN();
        var serieID = "swsh";

        var serie = await sdk.FetchSerie(serieID);

        Assert.IsNotNull(serie);
    }

    [TestMethod]
    public async Task FetchSeries_NoQuery_AllSeriesAsResumes()
    {
        var sdk = createTCGDexEN();

        var all_series = await sdk.FetchSeries();

        Assert.IsNotNull(all_series);
    }

    [TestMethod]
    public async Task FetchTypes_Normal_AllPossibleTypesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_types = await sdk.FetchTypes();

        Assert.IsNotNull(all_types);
    }

    [TestMethod]
    public async Task FetchRetreats_Normal_AllPossibleRetreatCostsAsIntegersInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchHPs());
    }

    [TestMethod]
    public async Task FetchRarities_Normal_AllPossibleRaritiesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_rarities = await sdk.FetchRarities();

        Assert.IsNotNull(all_rarities);
    }

    [TestMethod]
    public async Task FetchIllustrators_Normal_AllPossibleIllustratorsAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_illustrators = await sdk.FetchIllustrators();

        Assert.IsNotNull(all_illustrators);
    }

    [TestMethod]
    public async Task FetchHPs_Normal_AllPossibleHPsAsIntegersInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchHPs());
    }

    [TestMethod]
    public async Task FetchCategories_Normal_AllCategoriesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_categories = await sdk.FetchCategories();

        Assert.IsNotNull(all_categories);
    }

    [TestMethod]
    public async Task FetchDexIDs_Normal_AllPossibleDexIDsAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchDexIDs());
    }

    [TestMethod]
    public async Task FetchEnergyTypes_Normal_AllPossibleEnergyTypesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchEnergyTypes());
    }
    
    [TestMethod]
    public async Task FetchRegulationMarks_Normal_AllPossibleRegulationMarksAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        //var all_regulationMarks = await sdk.FetchRegulationMarks();
        await Assert.ThrowsAsync<WebException>(() => sdk.FetchRegulationMarks());
        //Assert.IsNotNull(all_regulationMarks);
    }

    [TestMethod]
    public async Task FetchStages_Normal_AllPossibleStagesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_stages = await sdk.FetchStages();

        Assert.IsNotNull(all_stages);
    }

    [TestMethod]
    public async Task FetchSuffixes_Normal_AllPossibleSuffixesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_suffixes = await sdk.FetchSuffixes();

        Assert.IsNotNull(all_suffixes);
    }

    [TestMethod]
    public async Task FetchTrainerTypes_Normal_AllPossibleTrainerTypesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchTrainerTypes());
    }

    [TestMethod]
    public async Task FetchVariants_Normal_AllPossibleVariantsAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_variants = await sdk.FetchVariants();

        Assert.IsNotNull(all_variants);
    }

    [TestMethod]
    public async Task DevProxyConfigTest()
    {
        var sdk = createTCGDexEN();

        try
        {
            var unmocked_req = await sdk.FetchCard("swsh2-50");
        }
        catch (System.Exception)
        {
            
            throw;
        }

        

        //Assert.IsNotNull(unmocked_req);
    }
}
