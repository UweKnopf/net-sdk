namespace net_sdk.src.models.subs;

public record class PricingTcgPlayerVariant(
    float? lowPrice,
    float? midPrice,
    float? highPrice,
    float? marketPrice,
    float? directLowPrice
);
