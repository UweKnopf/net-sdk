namespace net_sdk.src.models.subs;

public record class PricingCardMarket(
    string? Updated,
    string? Unit,
    float? Avg,
    float? Low,
    float? Trend,
    float? Avg1,
    float? Avg7,
    float? Avg30,
    float? LowHolo,
    float? TrendHolo,
    float? Avg1Holo,
    float? Avg7Holo,
    float? Avg30Holo
);
