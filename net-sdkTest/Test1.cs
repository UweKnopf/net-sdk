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
        var a = await sdk.fetchCard("swsh3-136");
        Assert.AreEqual("Pokemon", a.category);
        Assert.AreEqual("tetsuya koizumi", a.illustrator);
        Assert.AreEqual(110, a.hp);
        Assert.AreEqual("It makes a nest to suit its long and skinny body. The nest is impossible for other Pokémon to enter.", a.description);

        //Console.WriteLine(a.getImageUrl(Quality.low, Extension.jpg));

        Assert.AreEqual("https://assets.tcgdex.net/en/swsh/swsh3/136/low.jpg", a.getImageUrl(Quality.low, Extension.jpg));

        var img = a.getImage(Quality.low, Extension.jpg);
        Assert.IsNotNull(img);

        //File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "poster-got.jpg"), img);

        var b = await sdk.fetchCardResume("swsh3-136");
        //Console.WriteLine(a.ToString());

        var c = await b.getFullCard();
        Assert.AreEqual(expected: a.abilities, actual: c!.abilities);

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
    public async Task EndStateTest()
    {
        TCGDex sdk = new TCGDex(language: "en");
        //Basic fetch tests not throwing exceptions and returning something
        var card = await sdk.fetchCard("swsh3-136");
        var cardResume = await sdk.fetchCardResume("swsh3-136");
        var set = await sdk.fetchSet("swsh3");
        var serie = await sdk.fetchSeries("swsh");
        Assert.IsNotNull(card);
        Assert.IsNotNull(cardResume);
        Assert.IsNotNull(set);
        Assert.IsNotNull(serie);

        //Console.WriteLine(set.ToString());

        Assert.IsNotNull(card.getImage(Quality.low, Extension.jpg));
        Assert.IsNotNull(cardResume.getFullCard());
        Assert.IsNotNull(set.getLogo(Extension.jpg));
        Assert.IsNotNull(set.getSymbol(Extension.jpg));
        Assert.IsNotNull(serie.getLogo(Extension.jpg));
        /*
        //Dont test this to often because it might spam the api (?)
        var all_cards = await sdk.fetchCards();
        Console.WriteLine(all_cards.ToString());
        Console.WriteLine(all_cards.Count);
        Assert.IsNotNull(all_cards);
        Assert.IsGreaterThan(0, all_cards.Count);
        */

        var hpQuery = new Query(queryParameter: "hp", queryValue: "10");
        var nameQuery = new Query(queryParameter: "name", queryValue: "Clefairy Doll");

        var all_cards_query = await sdk.fetchCards(hpQuery, nameQuery);
        Console.WriteLine(all_cards_query!.Count);
        Assert.AreEqual("Clefairy Doll", all_cards_query[0].name);
        //Assert.IsGreaterThan(3, all_cards_query!.Count);
        
        
    }
}
