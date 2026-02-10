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
