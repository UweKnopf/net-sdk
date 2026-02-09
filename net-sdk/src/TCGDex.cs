using System;
using Microsoft.VisualBasic;
using net_sdk.src.models;
using RestSharp;

namespace net_sdk.src;

public interface ITCGDex
{
    Task<Card> fetchCard(string CardID);
}

public class TCGDex: ITCGDex {
    readonly RestClient _client;

    public TCGDex(string language)
    {
        var options = new RestClientOptions("https://api.tcgdex.net/v2/{language}");
        _client = new RestClient(options);
    }

    public async Task<Card> fetchCard(string CardID)
    {
        var response = await _client.GetAsync<Card>(
            "/cards/{CardID}",
            new { CardID }
        );
        return response!;
    }

    public void Dispose() {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }


    
}
