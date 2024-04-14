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

        private readonly string _jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TruthOrDare");

        public IEnumerable<Card> LoadCards(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                IEnumerable<Card> cards = new CardSerializer().Deserialize(json);
                return cards;
            }
            catch (IOException ex)
            {
                throw new PublicException("Failed to load cards. Cards file or folder not found.", ex);
            }
            catch (JsonException ex)
            {
                throw new PublicException("Failed to load cards. JSON parsing failed.", ex);
            }
        }

        public bool HasCards(string path)
        {
            return LoadCards(path).Any();
        }
    }
}
