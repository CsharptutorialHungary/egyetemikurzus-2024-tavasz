using System.Text.Json;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

namespace TruthOrDare.Infrastructure;
internal class JsonCardLoader
{
    internal IEnumerable<T> LoadCards<T>(string filePath) where T : ICard
    {
        try
        {
            var json = File.ReadAllText(filePath);

            IEnumerable<T> cards = new CardSerializer().Deserialize<T>(json);
            if (!cards.Any())
                throw new SafeException("Failed to load cards. No cards found.");
            return cards;
        }
        catch (IOException)
        {
            throw new SafeException("Failed to load cards. Cards file or folder not found.");
        }
        catch (JsonException)
        {
            throw new SafeException("Failed to load cards. JSON parsing failed.");
        }
        catch (ArgumentNullException)
        {
            throw new SafeException("Failed to load cards. JSON is null.");
        }
        catch (NotSupportedException)
        {
            throw new SafeException("Failed to load cards. JSON is not supported.");
        }
    }
}
