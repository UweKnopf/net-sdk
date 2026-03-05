using System;
using RestSharp;

namespace net_sdk.src;

public class Query
{
    private string totalQueryString = "?";

    public Query()
    {
        
    }

    public Query Equal(string parameter, string value)
    {
        totalQueryString += $"{parameter}={value}&";
        return this;
    }

    public Query StrictEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=eq:{value}&";
        return this;
    }

    public Query NotEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=not:{value}&";
        return this;
    }

    public Query StrictNotEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=neq:{value}&";
        return this;
    }

    public Query GreaterOrEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=gte:{value}&";
        return this;
    }

    public Query LesserOrEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=lte:{value}&";
        return this;
    }

    public Query Greater(string parameter, string value)
    {
        totalQueryString += $"{parameter}=gt:{value}&";
        return this;
    }

    public Query Lesser(string parameter, string value)
    {
        totalQueryString += $"{parameter}=lt:{value}&";
        return this;
    }

    public Query IsNull(string parameter)
    {
        totalQueryString += $"{parameter}=null:&";
        return this;
    }

    public Query IsNotNull(string parameter)
    {
        totalQueryString += $"{parameter}=notnull:&";
        return this;
    }

    public Query Sort(string parameter, SortOrders sortOrder)
    {
        switch (sortOrder)
        {
            case SortOrders.Ascending:
                totalQueryString += $"sort:field={parameter}&sort:order=ASC&";
                return this;
            case SortOrders.Descending:
                totalQueryString += $"sort:field={parameter}&sort:order=DESC&";
                return this;

            default:
                totalQueryString += $"sort:field={parameter}&sort:order=ASC&";
                return this;
        }
        
    }

    public Query Pagination(int page, int itemsPerPage)
    {
        totalQueryString += $"pagination:page={page}&pagination:itemsPerPage={itemsPerPage}";
        return this;
        
    }

    internal RestRequest ReturnRequestWithAllQueries(string fetchParam)
    {
        return new RestRequest(fetchParam + totalQueryString);
    }
}

public enum SortOrders
{
    Ascending,
    Descending
}
