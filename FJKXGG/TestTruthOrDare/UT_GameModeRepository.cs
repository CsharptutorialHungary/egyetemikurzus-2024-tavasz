using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TruthOrDare.Infrastructure.Repositories;
using TruthOrDare.Infrastructure;
using TruthOrDare.Domain.Entities;

namespace TestTruthOrDare;

public class UT_GameModeRepository
{
    private GameModeRepository _sut;

    private readonly List<GameMode> _gameModes = [
    new GameMode
        {
            Id = 0,
            Name = "Friends",
            Description = "The classic game mode."
        },
        new GameMode
        {
            Id = 1,
            Name = "Party",
            Description = "A new drinking game."
        },
        new GameMode
        {
            Id = 2,
            Name = "Romantic",
            Description = "Just for couples."
        }
    ];

    [SetUp]
    public void Setup()
    {
        _sut = new GameModeRepository();
    }

    [Test]
    public void EnsureThat_GetAllGameModes_WorksCorrect()
    {
        CollectionAssert.AreEqual(_sut.GetAllGameModes(), _gameModes);
    }

    [Test]
    public void EnsureThat_GetGameModeById_WorksCorrect()
    {
        Assert.That(_sut.GetGameModeById(0), Is.EqualTo(_gameModes[0]));
    }
}
