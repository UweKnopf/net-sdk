using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;
using net_sdk.src.internal_classes;
using net_sdk.src.models;
using RestSharp;

namespace net_sdk.src;

public interface ITCGDex
{
    Task<Card> fetchCard(string CardID);
    Task<CardResume> fetchCardResume(string CardId);
    Task<Set> fetchSet(string SetID);
    Task<Serie> fetchSeries(string SerieID);
}

public class TCGDex: ITCGDex, IDisposable{
    readonly RestClient _client;

    public TCGDex(string language)
    {
        var options = new RestClientOptions($"https://api.tcgdex.net/v2/{language}");
        _client = new RestClient(options);
        _client.AddDefaultHeader("user-agent", "@UweKnopf/net-sdk");
    }

    private async Task<T> fetch<T>(string fetchParam) where T : Model
    {
        var req = new RestRequest(fetchParam);
        var response = await _client.GetAsync<T>(req);
        //null handling?
        response!.tCGDex = this;
        return response;
    }

    private async Task<List<T>> fetchList<T>(string fetchParam, string queryParameter, string queryValue) where T : Model
    {
        //var a = fetchParam; //passing fetchParam directly dosnt work but this does????
        var req = new RestRequest(fetchParam);
        if (!string.IsNullOrEmpty(queryParameter) | !string.IsNullOrEmpty(queryValue)!)
        {
            req.AddQueryParameter(queryParameter, queryValue);
        }
        var response = await _client.GetAsync<List<T>>(req);

        //this looks horrid but I mean it works so its fine??
        foreach (var card in response!)
        {
            card.tCGDex = this;
        }
        
        return response;
    }

    public byte[]? getImage(string imageUrl)
    {
        //possible bug with relative vs absolute imageUrl path
        var fileBytes = this._client.DownloadData(new RestRequest(imageUrl, Method.Get));
        return fileBytes;
    }

    public void Dispose() {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<List<CardResume>?> fetchCards(string queryParameter = "", string queryValue = "")
    {
        var response = await fetchList<CardResume>("/cards", queryParameter, queryValue);
        return response;
    }


    public async Task<Card> fetchCard(string CardID)
    {
        var response = await fetch<Card>("/cards/" + CardID);
        return response;
    }

    public async Task<CardResume> fetchCardResume(string CardID)
    {
        var response = await fetch<CardResume>("/cards/" + CardID);
        return response;
    }

    public async Task<Set> fetchSet(string SetID)
    {
        var response = await fetch<Set>("/sets/" + SetID);
        return response;
    }

    public async Task<Serie> fetchSeries(string SerieID)
    {
        var response = await fetch<Serie>("/series/" + SerieID);
        return response;
    }
}
