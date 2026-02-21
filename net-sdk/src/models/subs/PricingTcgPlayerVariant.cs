namespace net_sdk.src.models.subs;

public record class PricingTcgPlayerVariant(
    float? LowPrice,
    float? MidPrice,
    float? HighPrice,
    float? MarketPrice,
    float? DirectLowPrice
);
