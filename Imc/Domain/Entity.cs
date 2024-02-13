using System.Text.Json.Serialization;

namespace Imc.Domain;

public abstract class Entity
{
    [JsonInclude]
    public Guid Id { get; protected set; } = Guid.NewGuid();
}