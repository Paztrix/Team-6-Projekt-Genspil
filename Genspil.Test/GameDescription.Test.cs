using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genspil;

namespace Genspil.Test
{
    [TestClass]
    public class GameDescriptionTests
    {
        /// <summary>
        /// Tester at alle default-værdier i GameDescription sættes korrekt, når kun navn angives.
        /// </summary>
        [TestMethod]
        public void GameDescription_DefaultValues_AreSetCorrectly()
        {
            // Arrange og Act
            var gameDescription = new GameDescription("Ludo");

            // Assert
            Assert.AreEqual("Ludo", gameDescription.Name);
            Assert.AreEqual("", gameDescription.Description); // Default tom streng
            Assert.AreEqual(0, gameDescription.MinAge); // Default alder
            Assert.AreEqual(0, gameDescription.MinPlayers); // Default min spillere
            Assert.AreEqual(99, gameDescription.MaxPlayers); // Default max spillere
            Assert.AreEqual(Genre.NA, gameDescription.GameGenre); // Default genre
        }
    }
}