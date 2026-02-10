namespace net_sdk.src.models.subs;

public record class PricingTcgPlayer(
    string updated,
    string unit,
    PricingTcgPlayerVariant? normal,
    PricingTcgPlayerVariant? holoFoil,
    PricingTcgPlayerVariant? reverseHolo,
    PricingTcgPlayerVariant? firstEdition,
    PricingTcgPlayerVariant? firstEditionHolofoil,
    PricingTcgPlayerVariant? unlimited,
    PricingTcgPlayerVariant? unlimitedHolofoil
);
