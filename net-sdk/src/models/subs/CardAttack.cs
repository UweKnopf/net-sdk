using System;

namespace net_sdk.src.models.subs;

public record class CardAttack(string Name, List<string>? Cost = null, string? Effect = null, int? Damage = null);
