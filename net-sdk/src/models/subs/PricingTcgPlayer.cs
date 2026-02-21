namespace net_sdk.src.models.subs;

public record class PricingTcgPlayer(
    string Updated,
    string Unit,
    PricingTcgPlayerVariant? Normal,
    PricingTcgPlayerVariant? HoloFoil,
    PricingTcgPlayerVariant? ReverseHolo,
    PricingTcgPlayerVariant? FirstEdition,
    PricingTcgPlayerVariant? FirstEditionHoloFoil,
    PricingTcgPlayerVariant? Unlimited,
    PricingTcgPlayerVariant? UnlimitedHoloFoil
);
