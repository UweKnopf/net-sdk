using System;

namespace net_sdk.src.models.subs;

public record class CardAttack(string name, List<String>? cost = null, string? effect = null, string? damage = null);
