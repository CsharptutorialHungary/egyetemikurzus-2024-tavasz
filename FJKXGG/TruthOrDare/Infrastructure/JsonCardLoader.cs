using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

namespace TruthOrDare.Infrastructure
{
    internal class JsonCardLoader
    {
        public IEnumerable<T> LoadCards<T>(string filePath) where T : ICard
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
        }
    }
}
