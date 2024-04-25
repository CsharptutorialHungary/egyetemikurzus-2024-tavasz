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
        // TODO: Make environmental variables settable for testability
        // TODO: Move environmental variables to other file, like config file
        private readonly string _defaultFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TruthOrDare");
        private readonly string _defaultTruthCardsFile = "TruthCards.json";
        private readonly string _defaultDareCardsFile = "DareCards.json";


        private readonly JsonCardLoader loader;
        private readonly JsonCardWriter writer;

        public CardRepository(JsonCardLoader loader, JsonCardWriter writer)
        {
            this.loader = loader;
            this.writer = writer;
        }

        public async Task GenerateDefaultCardsAsync()
        {
            await Task.Run(() => writer.GenerateDefaultCards(_defaultFolder, _defaultTruthCardsFile, _defaultDareCardsFile));
        }

        public async Task<IEnumerable<ICard>> GetAllCardsAsync()
        {
            // TODO: use list to store tasks
            Task<IEnumerable<TruthCard>> truthCardsTask = GetCardsByTypeAsync<TruthCard>();
            Task<IEnumerable<DareCard>> dareCardsTask = GetCardsByTypeAsync<DareCard>();

            await Task.WhenAll(truthCardsTask, dareCardsTask);

            IEnumerable<TruthCard> truthCards = truthCardsTask.Result;
            IEnumerable<DareCard> dareCards = dareCardsTask.Result;

            return truthCards.Concat<ICard>(dareCards);
        }


        public async Task<IEnumerable<T>> GetCardsByTypeAsync<T>() where T : ICard
        {
            // TODO: use reflections to load available card types
            if (typeof(T) == typeof(DareCard))
            {
                string dareCardsPath = Path.Combine(_defaultFolder, _defaultDareCardsFile);
                return await Task.Run(() => loader.LoadCards<DareCard>(dareCardsPath) as IEnumerable<T> ?? throw new SafeException("No card found."));
            }
            else if (typeof(T) == typeof(TruthCard))
            {
                string truthCardsPath = Path.Combine(_defaultFolder, _defaultTruthCardsFile);
                return await Task.Run(() => loader.LoadCards<TruthCard>(truthCardsPath) as IEnumerable<T> ?? throw new SafeException("No card found."));
            }
            else
            {
                throw new SafeException("Invalid Card type.");
            }
        }
    }
}
