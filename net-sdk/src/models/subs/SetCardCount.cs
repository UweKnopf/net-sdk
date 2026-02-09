namespace net_sdk.src.models.subs;

public record class SetCardCount
(
    int total,
    int official,
    int normal,
    int reverse,
    int holo,
    int? firstEd
);
