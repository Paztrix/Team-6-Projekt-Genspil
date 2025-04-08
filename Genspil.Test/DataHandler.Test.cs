using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Genspil;

namespace Genspil.Tests
{
    [TestClass]
    public class DataHandlerTests
    {
        private readonly string tempFilePath = Path.Combine(Path.GetTempPath(), "test_games.txt");

        [TestMethod]
        public void SaveAndLoadGames_ShouldPersistDataCorrectly()
        {
            // Setup midlertidig datamappe til test
            string tempTestDir = Path.Combine(Path.GetTempPath(), "GenspilTestData");
            Directory.CreateDirectory(tempTestDir);
            DataHandler.SetDataFolder(tempTestDir);

            // ARRANGE:
            // Opret to GameType-objekter med testdata, som senere skal bruges til at oprette Game-objekter
            var gameType1 = new GameType("Partners", "", 8, 2, 4, Genre.Strategi);
            var gameType2 = new GameType("Matador", "", 10, 2, 6, Genre.Familie);

            // Opret en liste af Game-objekter baseret på de to GameType-objekter
            var originalGames = new List<Game>
            {
                new Game(1, 100, gameType1, Condition.Perfekt),
                new Game(2, 150, gameType2, Condition.Brugsspor)
            };

            // ACT:
            // Gem spillene i en fil vha. SaveGames-metoden
            DataHandler.SaveGames(originalGames);

            // Indlæs spillene igen fra filen vha. LoadGames-metoden
            // De samme GameType-objekter gives med som reference, så de kan matches korrekt
            var loadedGames = DataHandler.LoadGames(new List<GameType> { gameType1, gameType2 });

            // ASSERT:
            // Tjek at antallet af indlæste spil matcher antallet af originale spil
            Assert.AreEqual(originalGames.Count, loadedGames.Count, "Antallet af spil matcher ikke.");

            // Gennemgå alle spil og tjek at hver enkelt egenskab er identisk mellem original og genindlæst spil
            for (int i = 0; i < originalGames.Count; i++)
            {
                Assert.AreEqual(originalGames[i].Id, loadedGames[i].Id);
                Assert.AreEqual(originalGames[i].price, loadedGames[i].price);
                Assert.AreEqual(originalGames[i].type.Name, loadedGames[i].type.Name);
                Assert.AreEqual(originalGames[i].type.GameGenre, loadedGames[i].type.GameGenre);
                Assert.AreEqual(originalGames[i].type.MinAge, loadedGames[i].type.MinAge);
                Assert.AreEqual(originalGames[i].type.MaxPlayers, loadedGames[i].type.MaxPlayers);
                Assert.AreEqual(originalGames[i].GameCondition, loadedGames[i].GameCondition);
            }

            // Ryd op efter test
            Directory.Delete(tempTestDir, recursive: true);
        }
    }
}
