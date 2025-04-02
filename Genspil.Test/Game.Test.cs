using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genspil;
using System;
using System.Collections.Generic;
using System.IO;

namespace Genspil.Test
{
    [TestClass]
    public class GameTests
    {
        /// <summary>
        /// Tester om konstruktøren korrekt sætter pris, condition (default) og gametype.
        /// </summary>
        [TestMethod]
        public void Game_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var gameType = new GameType("Catan", "A classic game", 10, 3, 4);

            // Act
            var game = new Game(1, 299.95, gameType);

            // Assert
            Assert.AreEqual(299.95, game.price);
            Assert.AreEqual(Condition.Perfect, game.GameCondition);
            Assert.AreEqual(gameType, game.type);
        }

        /// <summary>
        /// Tester om konstruktøren korrekt sætter en custom condition, når den angives eksplicit.
        /// </summary>
        [TestMethod]
        public void Game_Constructor_SetsCustomCondition()
        {
            // Arrange
            var gameType = new GameType("Risk", "Global domination", 12, 2, 6);

            // Act
            var game = new Game(2, 199.95, gameType, Condition.InWorkshop);

            // Assert
            Assert.AreEqual(Condition.InWorkshop, game.GameCondition);
        }

        /// <summary>
        /// Tester om DisplayGames() printer de korrekte oplysninger til konsollen, baseret på data i PseudoDatabase.games.
        /// </summary>
        [TestMethod]
        public void DisplayGames_PrintsExpectedOutput()
        {
            // Arrange
            var gameType = new GameType("Chess", "Classic game", 6, 2, 2);
            var game = new Game(3, 100.00, gameType);
            PseudoDatabase.games = new List<Game> { game };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Game.DisplayGames();

            // Assert
            var output = sw.ToString();
            Assert.IsTrue(output.Contains("Chess"));
            Assert.IsTrue(output.Contains("100"));
            Assert.IsTrue(output.Contains("Perfect"));
        }
    }
}
