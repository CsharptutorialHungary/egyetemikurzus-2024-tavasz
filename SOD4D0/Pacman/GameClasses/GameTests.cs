using NUnit.Framework;
using NUnit.Framework.Legacy; // Legacy Assert használata

using Pacman;

using System;

namespace Pacman.GameClasses
{
    [TestFixture]
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
            Game.IsTestEnvironment = true; // Tesztelési környezet beállítása
        }

        [Test]
        public void PlayerMovement_UpDirection_Success()
        {
            // Arrange
            Game.ResetGame();
            int initialPosY = Game.pacman.GetPosY();
            Game.GetKey = () => ConsoleKey.UpArrow;

            // Act
            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosY = Game.pacman.GetPosY();

            // Assert
            ClassicAssert.AreEqual(initialPosY - 1, newPosY);
        }

        [Test]
        public void PlayerMovement_DownDirection_Success()
        {
            // Arrange
            Game.ResetGame();
            int initialPosY = Game.pacman.GetPosY();
            Game.GetKey = () => ConsoleKey.DownArrow;

            // Act
            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosY = Game.pacman.GetPosY();

            // Assert
            ClassicAssert.AreEqual(initialPosY + 1, newPosY);
        }

        [Test]
        public void PlayerMovement_LeftDirection_Success()
        {
            // Arrange
            Game.ResetGame();
            int initialPosX = Game.pacman.GetPosX();
            Game.GetKey = () => ConsoleKey.LeftArrow;

            // Act
            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosX = Game.pacman.GetPosX();

            // Assert
            ClassicAssert.AreEqual(initialPosX - 1, newPosX);
        }

        [Test]
        public void PlayerMovement_RightDirection_Success()
        {
            // Arrange
            Game.ResetGame();
            int initialPosX = Game.pacman.GetPosX();
            Game.GetKey = () => ConsoleKey.RightArrow;

            // Act
            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosX = Game.pacman.GetPosX();

            // Assert
            ClassicAssert.AreEqual(initialPosX + 1, newPosX);
        }

        [Test]
        public void CheckIfNoLives_NoLives_GameOver()
        {
            // Arrange
            Game.ResetGame();
            Game.pacman.LoseLife();
            Game.pacman.LoseLife();

            // Act
            Game.CheckIfNoLives();

            // Assert
            ClassicAssert.IsFalse(Game.continueLoop);
        }

        [Test]
        public void CheckScore_Score684_WinGame()
        {
            // Arrange
            Game.ResetGame();
            Game.pacman.ResetScore();
            Game.pacman.IncreaseScore(684);

            // Act
            Game.CheckScore();

            // Assert
            ClassicAssert.IsFalse(Game.continueLoop);
        }

        [Test]
        public void ResetGame_SetsPacmanToInitialState()
        {
            // Arrange
            Game.pacman.LoseLife();
            Game.pacman.IncreaseScore(100);

            // Act
            Game.ResetGame();
            int initialPosX = Game.pacman.GetPosX();
            int initialPosY = Game.pacman.GetPosY();
            int lives = Game.pacman.Lives();
            int score = Game.pacman.Score;

            // Assert
            ClassicAssert.AreEqual(0, initialPosX);
            ClassicAssert.AreEqual(0, initialPosY);
            ClassicAssert.AreEqual(2, lives);
            ClassicAssert.AreEqual(0, score);
        }
    }
}
