using System.Text.Json;

using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Infrastructure
{
    internal class CardSerializer
    {
        public void Serialize(Stream target, IEnumerable<Card> instance)
        {
            JsonSerializer.Serialize(target, instance, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
            });
        }

        public IEnumerable<Card> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<IEnumerable<Card>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
            }) ?? throw new ArgumentNullException();
        }
    }
}
