using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genspil;

namespace Genspil.Test
{
    [TestClass]
    public class GameTypeTests
    {
        /// <summary>
        /// Tester at alle default-værdier i GameType sættes korrekt, når kun navn angives.
        /// </summary>
        [TestMethod]
        public void GameType_DefaultValues_AreSetCorrectly()
        {
            // Arrange og Act
            var gameType = new GameType("Ludo");

            // Assert
            Assert.AreEqual("Ludo", gameType.Name);
            Assert.AreEqual("", gameType.Description); // Default tom streng
            Assert.AreEqual(0, gameType.MinAge); // Default alder
            Assert.AreEqual(0, gameType.MinPlayers); // Default min spillere
            Assert.AreEqual(99, gameType.MaxPlayers); // Default max spillere
            Assert.AreEqual(Genre.NA, gameType.GameGenre); // Default genre
        }
    }
}