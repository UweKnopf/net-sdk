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
    Task<Serie> fetchSerie(string SerieID);
}

//not great ergonomics; maybe make caller supply enum based on T
public record class Query(string queryParameter, string queryValue);


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

    private async Task<List<T>> fetchList<T>(string fetchParam, params Query[] querys) where T : Model
    {
        //var a = fetchParam; //passing fetchParam directly dosnt work but this does????
        var req = new RestRequest(fetchParam);
        foreach (var query in querys)
        {
            if (!string.IsNullOrEmpty(query.queryParameter) | !string.IsNullOrEmpty(query.queryValue)!)
            {
                req.AddQueryParameter(query.queryParameter, query.queryValue);
            }
        }
        
        var response = await _client.GetAsync<List<T>>(req);

        //this looks horrid but I mean it works so its fine??
        foreach (var card in response!)
        {
            card.tCGDex = this;
        }
        
        return response;
    }

    private async Task<List<T>?> fetchSimpleList<T>(string fetchParam)
    {
        var req = new RestRequest(fetchParam);
        var response = await _client.GetAsync<List<T>>(req);
        
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

    public async Task<List<CardResume>?> fetchCards(params Query[] queries)
    {
        var response = await fetchList<CardResume>("/cards", queries);
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

    public async Task<List<SetResume>> fetchSets(params Query[] queries)
    {
        var response = await fetchList<SetResume>("/sets", queries);
        return response;
    }

    public async Task<Serie> fetchSerie(string SerieID)
    {
        var response = await fetch<Serie>("/series/" + SerieID);
        return response;
    }

    public async Task<List<Serie>> fetchSeries(params Query[] queries)
    {
        var response = await fetchList<Serie>("/series", queries);
        return response;
    }

    //endpoints for Listing purposes    
    public async Task<List<string>?> fetchTypes()
    {
        var response = await fetchSimpleList<string>("/types");
        return response;
    }

    public async Task<List<int>?> fetchRetreats()
    {
        var response = await fetchSimpleList<int>("/retreat");
        return response;
    }

    public async Task<List<string>?> fetchRarities()
    {
        var response = await fetchSimpleList<string>("/rarities");
        return response;
    }

    public async Task<List<string>?> fetchIllustrators()
    {
        var response = await fetchSimpleList<string>("/illustrators");
        return response;
    }

    public async Task<List<int>?> fetchHPs()
    {
        var response = await fetchSimpleList<int>("/hps");
        return response;
    }

    public async Task<List<string>?> fetchCategories()
    {
        var response = await fetchSimpleList<string>("/categories");
        return response;
    }

    public async Task<List<string>?> fetchDexIDs()
    {
        var response = await fetchSimpleList<string>("/dexids");
        return response;
    }

    public async Task<List<string>?> fetchEnergyTypes()
    {
        var response = await fetchSimpleList<string>("/energytypes");
        return response;
    }

    public async Task<List<string>?> fetchRegulationMarks()
    {
        var response = await fetchSimpleList<string>("/regulationmarks");
        return response;
    }

    public async Task<List<string>?> fetchStages()
    {
        var response = await fetchSimpleList<string>("/stages");
        return response;
    }

    public async Task<List<string>?> fetchSuffixes()
    {
        var response = await fetchSimpleList<string>("/suffixes");
        return response;
    }

    public async Task<List<string>?> fetchTrainerTypes()
    {
        var response = await fetchSimpleList<string>("/trainertypes");
        return response;
    }

    public async Task<List<string>?> fetchVariants()
    {
        var response = await fetchSimpleList<string>("/variants");
        return response;
    }

    
    
}
