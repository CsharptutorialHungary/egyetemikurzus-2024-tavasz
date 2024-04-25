using TruthOrDare.Domain.Entities;
using TruthOrDare.Infrastructure;
using TruthOrDare.Infrastructure.Repositories;

namespace TestTruthOrDare;

public class Tests
{
    CardRepository _sut;
    JsonCardLoader _loader;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EnsureThat_GetRandomCard_WorksCorrect()
    {
        //Arrange
        _sut = new CardRepository(_loader);

        //Act
        var result = _sut.GetRandomCard();

        //Assert
        Assert.IsAssignableFrom<Card>(result);
    }

    [Test]
    public void EnsureThat_GetCardById_WorksCorrect()
    {
        //Arrange
        _sut = new CardRepository(_loader);

        //Act
        var result = _sut.GetCardById(Guid.NewGuid());

        //Assert
        Assert.IsAssignableFrom<Card>(result);
    }
}