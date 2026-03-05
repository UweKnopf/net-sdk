using net_sdk.src.internal_classes;

namespace net_sdk.src.models;

public record class Serie(
    List<SetResume> Sets,
    string Id,
    string Name,
    string? Logo
    
) : SerieAbstract(Id, Name, Logo)
{
    
}
