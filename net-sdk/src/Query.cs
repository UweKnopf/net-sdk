using System;
using RestSharp;

namespace net_sdk.src;
/// <summary>
/// Class <c>Query</c> allows you to build Queries.
/// <example>
/// var tanks = new Query().Greater("hp", "100")
/// </example>
/// </summary>
public class Query
{
    private string totalQueryString = "?";

    public Query(){}

    /// <summary>
    /// Elements containing the <paramref name="value"/> for a certain <paramref name="parameter"/>.
    /// This is a Laxist Equality, so it's case insensitive (use <see cref="StrictEqual"/> for case sensitive ).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns><see cref="Query"/></returns>
    public Query Equal(string parameter, string value)
    {
        totalQueryString += $"{parameter}={value}&";
        return this;
    }

    /// <summary>
    /// Elements containing the exact <paramref name="value"/> for a certain <paramref name="parameter"/>.
    /// This is a strict Equality, so it's case sensitive  (use <see cref="Equal"/> for case insensitive).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns><see cref="Query"/></returns>
    public Query StrictEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=eq:{value}&";
        return this;
    }
    /// <summary>
    /// Elements not containing the <paramref name="value"/> for a certain <paramref name="parameter"/>.
    /// This is a Laxist Nonequality, so it's case insensitive (use <see cref="StrictEqual"/> for case sensitive ).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns><see cref="Query"/></returns>
    public Query NotEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=not:{value}&";
        return this;
    }
    /// <summary>
    /// Elements not containing the exact <paramref name="value"/> for a certain <paramref name="parameter"/>.
    /// This is strict Nonequal, so it's case sensitive (use <see cref="Equal"/> for case insensitive).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns><see cref="Query"/></returns>
    public Query StrictNotEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=neq:{value}&";
        return this;
    }
    /// <summary>
    /// Elements greater or equal than <paramref name="value"/> for <paramref name="parameter"/>.
    /// Only works for certain parameters (like Hp).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Query GreaterOrEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=gte:{value}&";
        return this;
    }

    /// <summary>
    /// Elements lesser or equal than <paramref name="value"/> for <paramref name="parameter"/>.
    /// Only works for certain parameters (like Hp).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Query LesserOrEqual(string parameter, string value)
    {
        totalQueryString += $"{parameter}=lte:{value}&";
        return this;
    }

    /// <summary>
    /// Elements greater than <paramref name="value"/> for <paramref name="parameter"/>.
    /// Only works for certain parameters (like Hp).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Query Greater(string parameter, string value)
    {
        totalQueryString += $"{parameter}=gt:{value}&";
        return this;
    }

    /// <summary>
    /// Elements lesser than <paramref name="value"/> for <paramref name="parameter"/>.
    /// Only works for certain parameters (like Hp).
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Query Lesser(string parameter, string value)
    {
        totalQueryString += $"{parameter}=lt:{value}&";
        return this;
    }
    /// <summary>
    /// Elements where <paramref name="parameter"/> is equal to null/not set.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public Query IsNull(string parameter)
    {
        totalQueryString += $"{parameter}=null:&";
        return this;
    }

    /// <summary>
    /// Elements where <paramref name="parameter"/> is not null.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public Query IsNotNull(string parameter)
    {
        totalQueryString += $"{parameter}=notnull:&";
        return this;
    }

    /// <summary>
    /// Sorts Elements based on <paramref name="parameter"/> and <paramref name="sortOrder"/>.
    /// Not available for some parameters.
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="sortOrder"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns a subset of the query.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="itemsPerPage"></param>
    /// <returns></returns>
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
