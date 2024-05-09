using System.Runtime.CompilerServices;
using System.Text.Json;
using TruthOrDare.Domain.Entities;

[assembly: InternalsVisibleTo("TestTruthOrDare")]

namespace TruthOrDare.Infrastructure;
internal class CardSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
    };
    
    internal void Serialize<T>(Stream target, IEnumerable<T> instance) where T : ICard =>
        JsonSerializer.Serialize(target, instance, _jsonSerializerOptions);

    internal IEnumerable<T> Deserialize<T>(string json) where T : ICard =>
        JsonSerializer.Deserialize<IEnumerable<T>>(json, _jsonSerializerOptions)
        ?? throw new ArgumentNullException();



    // TODO: violates something, because return type is changed
    internal string Serialize(IEnumerable<ICard> instance) => 
        JsonSerializer.Serialize<ICard>(instance, _jsonSerializerOptions);

    internal IEnumerable<ICard> Deserialize(string json) =>
        JsonSerializer.Deserialize<IEnumerable<ICard>>(json, _jsonSerializerOptions)
        ?? throw new ArgumentNullException();
}
