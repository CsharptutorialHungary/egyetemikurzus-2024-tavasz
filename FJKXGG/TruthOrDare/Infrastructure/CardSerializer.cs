using System.Text.Json;
using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Infrastructure;
internal class CardSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
    };
    internal void Serialize<T>(Stream target, IEnumerable<T> instance) where T : ICard
    {
        JsonSerializer.Serialize(target, instance, _jsonSerializerOptions);
    }

    internal IEnumerable<T> Deserialize<T>(string json) where T : ICard =>
        JsonSerializer.Deserialize<IEnumerable<T>>(json, _jsonSerializerOptions)
        ?? throw new ArgumentNullException();
}
