using System.Net;
using net_sdk.src;
using net_sdk.src.models;
using RestSharp;

namespace net_sdkTest;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public async Task TestMethod1()
    {
        TCGDex sdk = new TCGDex(language: "en");
        var a = await sdk.FetchCard("swsh3-136");
        Assert.AreEqual("Pokemon", a.Category);
        Assert.AreEqual("Furret", a.Name);
        Assert.AreEqual("tetsuya koizumi", a.Illustrator);
        Assert.AreEqual(110, a.Hp);
        Assert.AreEqual("It makes a nest to suit its long and skinny body. The nest is impossible for other Pokémon to enter.", a.Description);

        //Console.WriteLine(a.getImageUrl(Quality.low, Extension.jpg));

        Assert.AreEqual("https://assets.tcgdex.net/en/swsh/swsh3/136/low.jpg", a.GetImageUrl(Quality.low, Extension.jpg));

        var img = a.GetImage(Quality.low, Extension.jpg);
        Assert.IsNotNull(img);

        //File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "poster-got.jpg"), img);

        var b = await sdk.FetchCardResume("swsh3-136");
        //Console.WriteLine(a.ToString());

        var c = await b.GetFullCard();
        Assert.AreEqual(expected: a.Abilities, actual: c!.Abilities);

    }

    [TestMethod]
    public async Task TestMethod2()
    {
        RestClient client = new RestClient("https://api.tcgdex.net/v2/en");
        RestRequest request = new RestRequest("/cards/swsh3-136", Method.Get);
        
        RestResponse response = client.Execute(request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    }

    [TestMethod]
    public async Task TestFetchSets()
    {
        TCGDex sdk = new TCGDex(language: "en");
        var all_sets = await sdk.FetchSets();
        Console.WriteLine(all_sets!.Count);
        Assert.IsGreaterThan(199, all_sets!.Count);
        var cardCountQuery = new Query("cardCount.total", "160");
        var all_sets_query = await sdk.FetchSets(cardCountQuery);
        Console.WriteLine(all_sets_query!.Count);
        Assert.IsGreaterThan(0, all_sets_query!.Count);

    }

    [TestMethod]
    public async Task TestFetchSeries()
    {
        TCGDex sdk = new TCGDex(language: "en");
        var all_series = await sdk.FetchSeries();
        Console.WriteLine(all_series!.Count);
        Assert.IsGreaterThan(20, all_series!.Count);
        var cardCountQuery = new Query("name", "Mega Evolution");
        var all_sets_query = await sdk.FetchSeries(cardCountQuery);
        Console.WriteLine(all_sets_query!.Count);
        Assert.IsGreaterThan(0, all_sets_query!.Count);

    }

    [TestMethod]
    public async Task TestOtherEndpoints()
    {
        TCGDex sdk = new TCGDex(language: "en");
        var a = await sdk.FetchTypes();
        Console.WriteLine("Types: " + a!.Count);

        var b = sdk.FetchHPs();
        var c = sdk.FetchStages();
        var d = sdk.FetchIllustrators();
        var e = sdk.FetchCategories();
        var f = sdk.FetchDexIDs();
        var g = sdk.FetchEnergyTypes();
        var h = sdk.FetchRarities();
        var i = sdk.FetchRegulationMarks();
        var j = sdk.FetchRetreats();
        var k = sdk.FetchSuffixes();
        var l = sdk.FetchTrainerTypes();
        var m = sdk.FetchTypes();
        var n = sdk.FetchVariants();


    }

    [TestMethod]
    public async Task EndStateTest()
    {
        TCGDex sdk = new TCGDex(language: "en");
        //Basic fetch tests not throwing exceptions and returning something
        var card = await sdk.FetchCard("swsh3-136");
        var cardResume = await sdk.FetchCardResume("swsh3-136");
        var set = await sdk.FetchSet("swsh3");
        var serie = await sdk.FetchSerie("swsh");
        Assert.IsNotNull(card);
        Assert.IsNotNull(cardResume);
        Assert.IsNotNull(set);
        Assert.IsNotNull(serie);

        //Console.WriteLine(set.ToString());

        Assert.IsNotNull(card.GetImage(Quality.low, Extension.jpg));
        Assert.IsNotNull(cardResume.GetFullCard());
        Assert.IsNotNull(set.GetLogo(Extension.jpg));
        Assert.IsNotNull(set.GetSymbol(Extension.jpg));
        Assert.IsNotNull(serie.GetLogo(Extension.jpg));
        /*
        //Dont test this to often because it might spam the api (?)
        var all_cards = await sdk.FetchCards();
        Console.WriteLine(all_cards.ToString());
        Console.WriteLine(all_cards.Count);
        Assert.IsNotNull(all_cards);
        Assert.IsGreaterThan(0, all_cards.Count);
        */

        var hpQuery = new Query(QueryParameter: "hp", QueryValue: "10");
        var nameQuery = new Query(QueryParameter: "name", QueryValue: "Clefairy Doll");

        var all_cards_query = await sdk.FetchCards(hpQuery, nameQuery);
        Console.WriteLine(all_cards_query!.Count);
        Assert.AreEqual("Clefairy Doll", all_cards_query[0].Name);
        //Assert.IsGreaterThan(3, all_cards_query!.Count);
        
        
    }
}
