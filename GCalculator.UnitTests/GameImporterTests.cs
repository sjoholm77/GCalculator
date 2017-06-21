using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCalculator.Domain;
using Xunit;

namespace GCalculator.UnitTests
{
    public class GameImporterTests
    {
        [Fact]
        public void EmptyGameFileGeneratesEmptyImportedGame()
        {
            var stream = GenerateStreamFromString("{\"game\": {}}");
            var res = GameImporter.Import(stream);
            Assert.NotNull(res);
        }

        [Fact]
        public void GameFileWithEmptyDeckGeneratesImportedGameWith()
        {
            var stream = GenerateStreamFromString("{\"game\": {\"deck\": []}}");
            var res = GameImporter.Import(stream);
            Assert.NotNull(res.Deck);
            Assert.Empty(res.Deck);
        }

        [Fact]
        public void GameFileWithDeckContainingOneCardCreatesDeckWithOneCard()
        {
            var stream = GenerateStreamFromString("{\"deck\": [{\"name\": \"Skirmisher\", \"id\": \"S1\"}]}");
            var res = GameImporter.Import(stream);
            Assert.Equal(1, res.Deck.Count);
            Assert.Equal("Skirmisher", res.Deck[0].Name);
            Assert.Equal("S1", res.Deck[0].Id);
        }

        [Fact]
        public void GameFileWithDeckContainingTwoCardsCreatesDeckWithTwoCards()
        {
            var stream = GenerateStreamFromString("{\"deck\": [{\"name\": \"Skirmisher\", \"id\": \"S1\"}, {\"name\": \"Priestess\", \"id\": \"PF1\"}]}");
            var res = GameImporter.Import(stream);
            Assert.Equal(2, res.Deck.Count);
            Assert.Equal("Skirmisher", res.Deck[0].Name);
            Assert.Equal("S1", res.Deck[0].Id);
            Assert.Equal("Priestess", res.Deck[1].Name);
            Assert.Equal("PF1", res.Deck[1].Id);
        }

        [Fact]
        public void GameFileWithDeckAndOneEmptyTurnIsGeneratedCorrectly()
        {
            var stream = GenerateStreamFromString("{\"deck\": [{\"name\": \"Skirmisher\", \"id\": \"S1\"}], \"turns\": [{\"draws\": [], \"plays\":[]}]}");
            var res = GameImporter.Import(stream);
            Assert.Equal(1, res.Turns.Count);
            Assert.Equal(0, res.Turns[0].Draws.Count);
            Assert.Equal(0, res.Turns[0].Plays.Count);
        }

        [Fact]
        public void GameFileWithOneCardDrawnTurnOneIsGeneratedCorrectly()
        {
            var stream = GenerateStreamFromString("{\"deck\": [{\"name\": \"Skirmisher\", \"id\": \"S1\"}], \"turns\": [{\"draws\": [\"S1\"], \"plays\":[]}]}");
            var res = GameImporter.Import(stream);
            Assert.Equal(1, res.Turns[0].Draws.Count);
            Assert.Equal("S1", res.Turns[0].Draws[0]);
        }

        [Fact]
        public void GameFileGeneratedCorrectly()
        {
            var stream = GenerateStreamFromString("{\"deck\": [{\"name\": \"Skirmisher\", \"id\": \"S1\"}, {\"name\": \"Priestess\", \"id\": \"PF1\"}], \"turns\": [{\"draws\": [\"S1\"], \"plays\":[{\"id\": \"S1\", \"targets\": [\"OP2\"]}]}, {\"draws\": [\"PF1\"], \"plays\":[]}]}");
            var res = GameImporter.Import(stream);
            Assert.Equal("S1", res.Turns[0].Plays[0].Id);
            Assert.Equal("OP2", res.Turns[0].Plays[0].Targets[0]);
        }

        protected Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
