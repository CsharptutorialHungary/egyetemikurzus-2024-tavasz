using System.Runtime.CompilerServices;
using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

[assembly: InternalsVisibleTo("TestTruthOrDare")]

namespace TruthOrDare.Infrastructure.Repositories;

// TODO: Violates the Single Responsibility Principle
internal class CardRepository(JsonCardLoader loader, JsonCardWriter writer) : ICardRepositoryPort
{
    private readonly JsonCardLoader _loader = loader;
    private readonly JsonCardWriter _writer = writer;

    // TODO: Move default environmental variables to other file, like config file
    internal string FolderPath { get; init; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TruthOrDare");
    internal string TruthCardsFilePath { get; init; } = "TruthCards.json";
    internal string DareCardsFilePath { get; init; } = "DareCards.json";

    public async Task GenerateDefaultCardsAsync()
    {
        await Task.Run(() => _writer.GenerateDefaultCards(FolderPath, TruthCardsFilePath, DareCardsFilePath));
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
        // TODO: use reflection to load available card types
        if (typeof(T) == typeof(DareCard))
        {
            string dareCardsPath = Path.Combine(FolderPath, DareCardsFilePath);
            return await Task.Run(() => _loader.LoadCards<DareCard>(dareCardsPath) as IEnumerable<T> ?? throw new SafeException("No card found."));
        }
        else if (typeof(T) == typeof(TruthCard))
        {
            string truthCardsPath = Path.Combine(FolderPath, TruthCardsFilePath);
            return await Task.Run(() => _loader.LoadCards<TruthCard>(truthCardsPath) as IEnumerable<T> ?? throw new SafeException("No card found."));
        }
        else
        {
            throw new SafeException("Invalid Card type.");
        }
    }
}
