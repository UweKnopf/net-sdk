using System;
using RestSharp;

namespace net_sdk.src;

public class Query
{
    private string totalQueryString = "?";

    public Query()
    {
        
    }

    public void Equal(string parameter, string value)
    {
        totalQueryString += $"{parameter}={value}&";
    }

    public RestRequest ReturnRequestWithAllQueries(string fetchParam)
    {
        return new RestRequest(fetchParam + totalQueryString);
    }
}
