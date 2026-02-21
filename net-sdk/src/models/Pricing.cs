using net_sdk.src.internal_classes;
using net_sdk.src.models.subs;

namespace net_sdk.src.models;

public record class Pricing(
    PricingTcgPlayer? Tcgplayer,
    PricingCardMarket? Cardmarket
) : Model()
{

}
