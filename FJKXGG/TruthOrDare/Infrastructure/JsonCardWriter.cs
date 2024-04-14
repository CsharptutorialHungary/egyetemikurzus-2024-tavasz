using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

namespace TruthOrDare.Infrastructure
{
    internal class JsonCardWriter
    {
        private readonly string _jsonFilePath;
        private readonly IGameModeRepositoryPort _gameModeRepository;
        public JsonCardWriter(IGameModeRepositoryPort gameModeRepository, string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
            _gameModeRepository = gameModeRepository;
        }
        public JsonCardWriter(IGameModeRepositoryPort gameModeRepository)
        {
            _gameModeRepository = gameModeRepository;
            _jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TruthOrDare", "Cards.json");
        }

        public void GenerateDefaultCards()
        {
            List<Card> cards = new List<Card>();
            foreach (var mode in _gameModeRepository.GetAllGameModes())
            {
                cards.Add(mode.Id % 2 == 1 ?
                    new TruthCard
                    {
                        Id = mode.Id,
                        Text = $"{mode.Name} card example text",
                        GameMode = mode,
                    }
                    :
                    new DareCard
                    {
                        Id = mode.Id,
                        Text = $"{mode.Name} card example text",
                        GameMode = mode,
                    }
                );
            }

            try
            {
                using var stream = File.Create(_jsonFilePath);
                try
                {
                    var serializer = new CardSerializer();
                    serializer.Serialize(stream, cards);
                }
                catch (IOException ex)
                {
                    throw new PublicException("Failed to write JSON file.", ex);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                // TODO: Log error
                string? dir = Path.GetDirectoryName(_jsonFilePath);
                if (dir != null)
                {
                    Directory.CreateDirectory(dir);
                    GenerateDefaultCards();
                } else
                {
                    throw new PublicException("Failed to create default card directory. Wrong path.");
                }
            }
        }
    }
}
