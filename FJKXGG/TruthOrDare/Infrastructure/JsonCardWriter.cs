using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

namespace TruthOrDare.Infrastructure;

// TODO: Violates the Single Responsibility Principle
internal class JsonCardWriter(IGameModeRepositoryPort gameModeRepository)
{
    private readonly IGameModeRepositoryPort _gameModeRepository = gameModeRepository;

    internal void GenerateDefaultCards(string folderPath, string truthFilePath, string dareFilePath)
    {
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        IEnumerable<TruthCard> defaultTruthCards = GetDefaultTruthCards();
        OverwriteCards(defaultTruthCards, Path.Combine(folderPath, truthFilePath));

        IEnumerable<DareCard> defaultDareCards = GetDefaultDareCards();
        OverwriteCards(defaultDareCards, Path.Combine(folderPath, dareFilePath));
    }

    private IEnumerable<TruthCard> GetDefaultTruthCards()
    {
        var classic = _gameModeRepository.GetGameModeById(0);
        var party = _gameModeRepository.GetGameModeById(1);
        var romantic = _gameModeRepository.GetGameModeById(2);
        List<TruthCard> cards = [
            new TruthCard ( 0,  "What was the most embarrassing moment in your life?", classic ),
            new TruthCard ( 1,  "What is the wildest thing you have ever done in your life so far?", classic ),
            new TruthCard ( 2,  "What was the last lie you told?", classic ),

            new TruthCard ( 3,  "Have you ever stolen from school or work?", party ),
            new TruthCard ( 4,  "What is the stupidest thing you've ever done while drunk?", party ),
            new TruthCard ( 5,  "If you had to murder someone, who would it be?", party ),

            new TruthCard ( 5,  "What's your idea of a perfect, romantic date?", romantic ),
            new TruthCard ( 6,  "What is the worst/the best quality of your girlfriend or boyfriend?", romantic ),
            new TruthCard ( 7,  "What's the strangest place you've ever had sex?", romantic )
            ];
        return cards;
    }

    private IEnumerable<DareCard> GetDefaultDareCards()
    {
        var classic = _gameModeRepository.GetGameModeById(0);
        var party = _gameModeRepository.GetGameModeById(1);
        var romantic = _gameModeRepository.GetGameModeById(2);
        List<DareCard> cards = [
            new DareCard ( 0,  "Try to lick your nose.", classic ),
            new DareCard ( 1,  "Dance with the person on your right to a song chosen by others.", classic ),
            new DareCard ( 2,  "Open the window, lean out, and loudly sing the national anthem!", classic ),

            new DareCard ( 3,  "Exchange a clothing item with the player on your right.", party ),
            new DareCard ( 4,  "Carry the player on your left around the room once.", party ),
            new DareCard ( 5,  "Imitate an animal chosen by the group.", party ),

            new DareCard ( 5,  "Give the person sitting across from you a lap dance for 10 seconds.", romantic ),
            new DareCard ( 6,  "Pretend I'm a stranger at a bar. Try to pick me up and convince me to come home with you.", romantic ),
            new DareCard ( 7,  "Massage your partner's butt.", romantic )
];
        return cards;
    }

    internal void OverwriteCards(IEnumerable<ICard> cards, string filePath)
    {
        try
        {
            using var stream = File.Create(filePath);
            try
            {
                var serializer = new CardSerializer();
                serializer.Serialize(stream, cards);
            }
            catch (IOException)
            {
                throw new SafeException("Failed to write JSON file.");
            }
        }
        catch (DirectoryNotFoundException)
        {
            throw new SafeException("Failed to write card files. Wrong path.");
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new SafeException("Failed to write card files. " + ex.Message);
        }
    }
}
