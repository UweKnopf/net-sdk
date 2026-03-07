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
    public async Task GetImage_CorrectImageURL_ReturnBytes()
    {
        var sdk = createTCGDexEN();
        var imageUrl = "https://assets.tcgdex.net/en/bw/bw6/1/low.png";

        var imageBytes = await sdk.GetImage(imageUrl);

        Assert.IsNotNull(imageBytes);
        Assert.IsInstanceOfType(imageBytes, typeof(byte[]));
    }

    [TestMethod]
    public async Task GetImage_WrongImageURL_()
    {
        var sdk = createTCGDexEN();
        var imageUrl = "";

        var imageBytes = await sdk.GetImage(imageUrl);

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
    public async Task FetchCards_WithSimpleQuery_PikachuCardsAsResumes()
    {
        var sdk = createTCGDexEN();

        var query = new Query().Equal("name", "pikachu");

        var all_pikachu_cardResumes = await sdk.FetchCards(query);

        Assert.IsNotNull(all_pikachu_cardResumes);
        Assert.IsGreaterThan(10, all_pikachu_cardResumes.Count);
        Assert.IsInstanceOfType(all_pikachu_cardResumes, typeof(List<CardResume>));
        Assert.IsNotNull(all_pikachu_cardResumes[0]);
        Assert.Contains("Pikachu", all_pikachu_cardResumes[0].Name);
    }

    [TestMethod]
    public async Task FetchCards_WithMultiQuery_PikachuCardWithSpecificIDAsResumes()
    {
        var sdk = createTCGDexEN();

        var query = new Query().Equal("name", "pikachu").Equal("id", "fut2020-1");

        var all_pikachu_cardResumes = await sdk.FetchCards(query);

        Assert.IsNotNull(all_pikachu_cardResumes);
        Assert.IsGreaterThan(0, all_pikachu_cardResumes.Count);
        Assert.IsInstanceOfType(all_pikachu_cardResumes, typeof(List<CardResume>));
        Assert.IsNotNull(all_pikachu_cardResumes[0]);
        Assert.Contains("Pikachu", all_pikachu_cardResumes[0].Name);
        Assert.Contains("fut2020-1", all_pikachu_cardResumes[0].Id);
    }

    [TestMethod]
    public async Task FetchCards_WithHPGreaterThanQuery_CardsWithGreaterThan100HPAsResumes()
    {
        var sdk = createTCGDexEN();

        var query = new Query().Greater("hp", "100");

        var tanks = await sdk.FetchCards(query);
        var firstTank = await tanks[0].GetFullCard();
        int firstTankHp = (int)firstTank.Hp;

        Assert.IsNotNull(tanks);
        Assert.IsNotEmpty(tanks);
        Assert.IsGreaterThan(100, firstTankHp);
    }

    [TestMethod]
    public async Task FetchCards_SortAndNameQuery_CardsWithPickachuSortedBasedOnHpAceAsResumes()
    {
        var sdk = createTCGDexEN();


        var query = new Query().Equal("name", "pikachu").Sort("hp", SortOrders.Ascending);

        var pickachuSorted = await sdk.FetchCards(query);

        var first = await pickachuSorted[0].GetFullCard();
        var second = await pickachuSorted[1].GetFullCard();

        Assert.IsNotNull(pickachuSorted);
        Assert.IsNotEmpty(pickachuSorted);
        Assert.IsGreaterThanOrEqualTo((int)first.Hp, (int)second.Hp);
    }

    [TestMethod]
    public async Task FetchCards_InlineSortAndNameQuery_CardsWithPickachuSortedBasedOnHpAceAsResumes()
    {
        var sdk = createTCGDexEN();
        

        var pickachuSorted = await sdk.FetchCards(new Query().Equal("name", "pikachu").Sort("hp", SortOrders.Ascending));

        var first = await pickachuSorted[0].GetFullCard();
        var second = await pickachuSorted[1].GetFullCard();

        Assert.IsNotNull(pickachuSorted);
        Assert.IsNotEmpty(pickachuSorted);
        Assert.IsGreaterThanOrEqualTo((int)first.Hp, (int)second.Hp);
    }

    [TestMethod]
    public async Task FetchCards_SortAndNameQueryUsingCache_CardsWithPickachuSortedBasedOnHpAceAsResumes()
    {
        var sdk = createTCGDexEN();


        var query = new Query().Equal("name", "pikachu").Sort("hp", SortOrders.Ascending);

        var pickachuSorted = await sdk.FetchCards(query);

        var first = await pickachuSorted[0].GetFullCard();
        var second = await pickachuSorted[1].GetFullCard();

        Assert.IsNotNull(pickachuSorted);
        Assert.IsNotEmpty(pickachuSorted);
        Assert.IsGreaterThanOrEqualTo((int)first.Hp, (int)second.Hp);

        var pickachuSorted_cached = await sdk.FetchCards(query);

        var first_cached = await pickachuSorted_cached[0].GetFullCard();
        var second_cached = await pickachuSorted_cached[1].GetFullCard();

        Assert.AreEqual(first, first_cached);
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
    public async Task FetchCard_UsingCache_Card()
    {
        var sdk = createTCGDexEN();
        var cardID = "swsh3-136";

        var card = await sdk.FetchCard(cardID);

        Assert.IsNotNull(card);
        Assert.IsNotNull(card.Name);

        var cached_card = await sdk.FetchCard(cardID);
        Assert.AreEqual(card, cached_card);
    }

    [TestMethod]
    public async Task FetchCard_UsingCacheButCacheEntryExpires_Card()
    {
        var sdk = createTCGDexEN();
        var cardID = "swsh3-136";
        sdk.SetCacheTTL(0.1);

        var card = await sdk.FetchCard(cardID);

        Assert.IsNotNull(card);
        Assert.IsNotNull(card.Name);
        Thread.Sleep(10000);
        var cached_card = await sdk.FetchCard(cardID);
        Assert.AreNotEqual(card, cached_card);
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
    public async Task FetchSets_NoQueryUsingCache_AllSetAsResumes()
    {
        var sdk = createTCGDexEN();

        var all_sets = await sdk.FetchSets();

        Assert.IsNotNull(all_sets);

        var cached_sets = await sdk.FetchSets();

        Assert.IsNotNull(cached_sets);

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
/*
    [TestMethod]
    public async Task FetchRetreats_Normal_AllPossibleRetreatCostsAsIntegersInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchHPs());
    }
*/
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
/*
    [TestMethod]
    public async Task FetchHPs_Normal_AllPossibleHPsAsIntegersInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchHPs());
    }
*/
    [TestMethod]
    public async Task FetchCategories_Normal_AllCategoriesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_categories = await sdk.FetchCategories();

        Assert.IsNotNull(all_categories);
    }
/*
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
*/
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
/*
    [TestMethod]
    public async Task FetchTrainerTypes_Normal_AllPossibleTrainerTypesAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        await Assert.ThrowsAsync<WebException>(() => sdk.FetchTrainerTypes());
    }
*/
    [TestMethod]
    public async Task FetchVariants_Normal_AllPossibleVariantsAsStringsInAList()
    {
        var sdk = createTCGDexEN();

        var all_variants = await sdk.FetchVariants();

        Assert.IsNotNull(all_variants);
    }

    /*
    //Test for proper workflow config of dev proxy
    [TestMethod]
    public async Task DevProxyConfigTest()
    {
        var sdk = createTCGDexEN();

        try
        {
            var unmocked_req = await sdk.FetchCard("swsh2-50");
            //supposed to fail if not using api mock
            Assert.IsNull(unmocked_req);
        }
        catch (WebException e)
        {
            
            Console.WriteLine(e);
        }

        
    }
    */

    
}
