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
}

public class TCGDex: ITCGDex, IDisposable{
    readonly RestClient _client;

    public TCGDex(string language)
    {
        var options = new RestClientOptions($"https://api.tcgdex.net/v2/{language}");
        _client = new RestClient(options);
        _client.AddDefaultHeader("user-agent", "@UweKnopf/net-sdk");

        
    }

    public async Task<Card> fetchCard(string CardID)
    {
        var response = await fetch<Card>(CardID);
        return response;
    }

    public async Task<CardResume> fetchCardResume(string CardID)
    {
        var response = await fetch<CardResume>(CardID);
        return response!;
    }

    public void Dispose() {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task<Model> fetch<Model>(string fetchParam)
    {
        var response = await _client.GetAsync<Model>(
            $"/cards/{fetchParam}",
            new { fetchParam }
        );
        //var a =
        //null handling?
        response!;
        return response!;
    }


    
}
