namespace net_sdk.src.models.subs;

public record class PricingCardMarket(
    string? updated,
    string? unit,
    float? avg,
    float? low,
    float? trend,
    float? avg1,
    float? avg7,
    float? avg30,
    //Serializedname 
    float? lowHolo,
    float? trendHolo,
    float? avg1Holo,
    float? avg7Holo,
    float? avg30Holo
);
