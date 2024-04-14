using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

[assembly: InternalsVisibleTo("TestTruthOrDare")]

namespace TruthOrDare.Infrastructure
{
    internal class CardRepository : ICardRepositoryPort
    {
        private readonly string _defaultFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TruthOrDare");
        // Find for the source variable a better place, like a configuration file

        private readonly JsonCardLoader loader;
        private readonly JsonCardWriter writer;

        public CardRepository(JsonCardLoader loader, JsonCardWriter writer)
        {
            this.loader = loader;
            this.writer = writer;
        }

        public void GenerateDefaultCards()
        {
            writer.GenerateDefaultCards();
        }

        public IEnumerable<Card> GetAllCards()
        {
            var truthCardsPath = Path.Combine(_defaultFolder, "TruthCards.json");
            var dareCardsPath = Path.Combine(_defaultFolder, "DareCards.json");

            return GetCardsByType<TruthCard>(truthCardsPath).Concat<Card>(GetCardsByType<DareCard>(dareCardsPath));
        }

        private IEnumerable<T> GetCardsByType<T>(string path) where T : Card
        {
            // TODO: Null check
            var cards = loader.LoadCards(path) as IEnumerable<T>;
            return cards;
        }

       
        public IEnumerable<T> GetCardsByType<T>() where T : Card
        {
            var truthCardsPath = Path.Combine(_defaultFolder, "TruthCards.json");
            var dareCardsPath = Path.Combine(_defaultFolder, "DareCards.json");

            if (typeof(T) == typeof(DareCard))
            {
                return GetCardsByType<T>(dareCardsPath);
            }
            else if (typeof(T) == typeof(TruthCard))
            {
                return GetCardsByType<T>(truthCardsPath);
            }
            else
            {
                throw new ArgumentException("Invalid card type");
            }
        }

        public Card GetCardById(int id)
        {
            throw new NotImplementedException();
        }

        public bool HasCards() => loader.HasCards(Path.Combine(_defaultFolder, "TruthCards.json")) && loader.HasCards(Path.Combine(_defaultFolder, "DareCards.json"));

    }
}
