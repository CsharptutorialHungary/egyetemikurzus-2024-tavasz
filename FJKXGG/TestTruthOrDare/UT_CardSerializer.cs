using TruthOrDare.Infrastructure;
using TruthOrDare.Infrastructure.Repositories;
using TruthOrDare.Domain.Entities;
using System.Text.Json;

namespace TestTruthOrDare
{

    internal class UT_CardSerializer
    {
        private CardSerializer _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardSerializer();
        }

        [Test]
        public void EnsureThat_Deserialize_WorksCorrect()
        {
            string cardJson = """[{"id":0,"text":"What was the most embarrassing moment in your life?","game-mode":{"id":0,"name":"Friends","description":"The classic game mode."}}]""";

            IEnumerable<TruthCard> cardObject = [new(0, "What was the most embarrassing moment in your life?", new GameMode
            {
                Id = 0,
                Name = "Friends",
                Description = "The classic game mode."
            })];
            Assert.That(_sut.Deserialize<TruthCard>(cardJson), Is.EqualTo(cardObject));

        }

        [Test]
        public void EnsureThat_Deserialize_ThrowsJsonException_WhenJsonIsEmpty()
        {
            Assert.Throws<JsonException>(() => _sut.Deserialize<TruthCard>(""));
        }

        [Test]
        public void EnsureThat_Deserialize_ThrowsArgumentNullException_WhenJsonIsEmptyArray()
        {
            IEnumerable<TruthCard> cardObject = [];
            Assert.That(_sut.Deserialize<TruthCard>("[]"), Is.EqualTo(cardObject));
        }

        [Test]
        public void EnsureThat_Deserialize_ThrowsJsonException_WhenJsonIsEmptyObject()
        {
            Assert.Throws<JsonException>(() => _sut.Deserialize<TruthCard>("{}"));
        }

        [Test]
        public void EnsureThat_Deserialize_ThrowsJsonException_WhenJsonIsInvalid()
        {
            Assert.Throws<JsonException>(() => _sut.Deserialize<TruthCard>("kismacska"));
        }

        [Test]
        public void EnsureThat_SerializeWithStream_WorksCorrect()
        {
            using MemoryStream stream = new();
            IEnumerable<TruthCard> cardObject = [new(0, "What was the most embarrassing moment in your life?", new GameMode
                           {
                Id = 0,
                Name = "Friends",
                Description = "The classic game mode."
            })];

            _sut.Serialize(stream, cardObject);
            stream.Position = 0;
            using StreamReader reader = new(stream);
            string cardJson = reader.ReadToEnd();

            Assert.That(cardJson, Is.EqualTo("""[{"id":0,"text":"What was the most embarrassing moment in your life?","game-mode":{"id":0,"name":"Friends","description":"The classic game mode."}}]"""));
        }
    }
}
