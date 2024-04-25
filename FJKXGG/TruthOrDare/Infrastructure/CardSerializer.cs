using System.Text.Json;

using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Infrastructure
{
    internal class CardSerializer
    {
        public void Serialize<T>(Stream target, IEnumerable<T> instance) where T : ICard
        {
            JsonSerializer.Serialize(target, instance, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
            });
        }

        public IEnumerable<T> Deserialize<T>(string json) where T : ICard
        {
            return JsonSerializer.Deserialize<IEnumerable<T>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
            }) ?? throw new ArgumentNullException();
        }
    }
}
