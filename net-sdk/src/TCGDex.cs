using System.Net;
using net_sdk.src.internal_classes;
using net_sdk.src.models;
using RestSharp;

namespace net_sdk.src;

public interface ITCGDex
{
    Task<Card> FetchCard(string cardId);
    Task<CardResume> FetchCardResume(string cardId);
    Task<Set> FetchSet(string setId);
    Task<Serie> FetchSerie(string serieId);
}

public record class Query(string QueryParameter, string QueryValue);

public class TCGDex: ITCGDex, IDisposable
{
    private readonly RestClient _client;

    public TCGDex(string language)
    {
        var options = new RestClientOptions($"https://api.tcgdex.net/v2/{language}");
        _client = new RestClient(options);
        _client.AddDefaultHeader("user-agent", "@UweKnopf/net-sdk");
    }

    private async Task<T> Fetch<T>(string fetchParam) where T : Model
    {
        var req = new RestRequest(fetchParam);
        var response = await _client.GetAsync<T>(req);
        response!.TCGDex = this;
        return response;
    }

    private async Task<List<T>> FetchList<T>(string fetchParam, params Query[] queries) where T : Model
    {
        var req = new RestRequest(fetchParam);
        foreach (var query in queries)
        {
            if (!string.IsNullOrEmpty(query.QueryParameter) && !string.IsNullOrEmpty(query.QueryValue))
            {
                req.AddQueryParameter(query.QueryParameter, query.QueryValue);
            }
        }
        
        var response = await _client.GetAsync<List<T>>(req);
        foreach (var card in response!)
        {
            card.TCGDex = this;
        }
        
        return response;
    }

    private async Task<List<T>?> FetchSimpleList<T>(string fetchParam)
    {
        var req = new RestRequest(fetchParam);
        /*
        This is probably not the way to do this but we first need to think about what (if any) errors we want to return to caller.
        Might be better to not use getasync and instead separate response and deserialization

        In that case the error thrown from restsharp would be more meaningful or, if none is thrown, we could do it ourself
        */
        try
        {
            var response = await _client.GetAsync<List<T>>(req);
            return response;
        }
        catch (System.Exception)
        {
            if (_client.Execute(req).StatusCode == HttpStatusCode.NotFound)
            {
                throw new WebException("The resource you are trying to reach does not exists");
            }
            else
            {
                throw;
            }
            
        }
        
        
    }

    public byte[]? GetImage(string imageUrl)
    {
        var fileBytes = _client.DownloadData(new RestRequest(imageUrl, Method.Get));
        return fileBytes;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<List<CardResume>?> FetchCards(params Query[] queries)
    {
        var response = await FetchList<CardResume>("/cards", queries);
        return response;
    }

    public async Task<Card> FetchCard(string cardId)
    {
        var response = await Fetch<Card>("/cards/" + cardId);
        return response;
    }

    public async Task<CardResume> FetchCardResume(string cardId)
    {
        var response = await Fetch<CardResume>("/cards/" + cardId);
        return response;
    }

    public async Task<Set> FetchSet(string setId)
    {
        var response = await Fetch<Set>("/sets/" + setId);
        return response;
    }

    public async Task<List<SetResume>> FetchSets(params Query[] queries)
    {
        var response = await FetchList<SetResume>("/sets", queries);
        return response;
    }

    public async Task<Serie> FetchSerie(string serieId)
    {
        var response = await Fetch<Serie>("/series/" + serieId);
        return response;
    }

    public async Task<List<Serie>> FetchSeries(params Query[] queries)
    {
        var response = await FetchList<Serie>("/series", queries);
        return response;
    }

    public async Task<List<string>?> FetchTypes()
    {
        var response = await FetchSimpleList<string>("/types");
        return response;
    }

    public async Task<List<int>?> FetchRetreats()
    {
        var response = await FetchSimpleList<int>("/retreat");
        return response;
    }

    public async Task<List<string>?> FetchRarities()
    {
        var response = await FetchSimpleList<string>("/rarities");
        return response;
    }

    public async Task<List<string>?> FetchIllustrators()
    {
        var response = await FetchSimpleList<string>("/illustrators");
        return response;
    }

    public async Task<List<int>?> FetchHPs()
    {
        var response = await FetchSimpleList<int>("/hps");
        return response;
    }

    public async Task<List<string>?> FetchCategories()
    {
        var response = await FetchSimpleList<string>("/categories");
        return response;
    }

    public async Task<List<string>?> FetchDexIDs()
    {
        var response = await FetchSimpleList<string>("/dexids");
        return response;
    }

    public async Task<List<string>?> FetchEnergyTypes()
    {
        var response = await FetchSimpleList<string>("/energytypes");
        return response;
    }

    public async Task<List<string>?> FetchRegulationMarks()
    {
        var response = await FetchSimpleList<string>("/regulationmarks");
        return response;
    }

    public async Task<List<string>?> FetchStages()
    {
        var response = await FetchSimpleList<string>("/stages");
        return response;
    }

    public async Task<List<string>?> FetchSuffixes()
    {
        var response = await FetchSimpleList<string>("/suffixes");
        return response;
    }

    public async Task<List<string>?> FetchTrainerTypes()
    {
        var response = await FetchSimpleList<string>("/trainertypes");
        return response;
    }

    public async Task<List<string>?> FetchVariants()
    {
        var response = await FetchSimpleList<string>("/variants");
        return response;
    }
}
