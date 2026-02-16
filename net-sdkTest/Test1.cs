using System.Net;
using net_sdk.src;
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
        Assert.AreEqual("Furret", a.name);

        //Console.WriteLine(a.getImageUrl(Quality.low, Extension.jpg));

        Assert.AreEqual("https://assets.tcgdex.net/en/swsh/swsh3/136/low.jpg", a.getImageUrl(Quality.low, Extension.jpg));

        var img = a.getImage(Quality.low, Extension.jpg);
        Assert.IsNotNull(img);

        //File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "poster-got.jpg"), img);

        var b = await sdk.fetchCardResume("swsh3-136");
        Console.WriteLine(b.name);

        var c = await b.getFullCard();
        Assert.AreEqual(expected: a.ToString(), actual: c!.ToString());

    }

    [TestMethod]
    public void TestMethod2()
    {
        RestClient client = new RestClient("https://api.tcgdex.net/v2");
        RestRequest request = new RestRequest("/en/cards/swsh3-136", Method.Get);
        
        RestResponse response = client.Execute(request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
