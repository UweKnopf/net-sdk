namespace net_sdk.src.models.subs;

public record class SetCardCount
(
    int Total,
    int Official,
    int Normal,
    int Reverse,
    int Holo,
    int? FirstEd
);
