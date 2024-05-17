using NUnit.Framework;
using NUnit.Framework.Legacy;

using Pacman.GameClasses;

using System;

namespace Pacman.Tests
{
    [TestFixture]
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
            Game.IsTestEnvironment = true;
        }

        [Test]
        public void PlayerMovement_UpDirection_Success()
        {
            Game.ResetGame();
            int initialPosY = Game.pacman.GetPosY();
            Game.GetKey = () => ConsoleKey.UpArrow;

            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosY = Game.pacman.GetPosY();

            ClassicAssert.AreEqual(initialPosY - 1, newPosY);
        }

        [Test]
        public void PlayerMovement_DownDirection_Success()
        {
            Game.ResetGame();
            int initialPosY = Game.pacman.GetPosY();
            Game.GetKey = () => ConsoleKey.DownArrow;

            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosY = Game.pacman.GetPosY();

            ClassicAssert.AreEqual(initialPosY + 1, newPosY);
        }

        [Test]
        public void PlayerMovement_LeftDirection_Success()
        {
            Game.ResetGame();
            int initialPosX = Game.pacman.GetPosX();
            Game.GetKey = () => ConsoleKey.LeftArrow;

            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosX = Game.pacman.GetPosX();

            ClassicAssert.AreEqual(initialPosX - 1, newPosX);
        }

        [Test]
        public void PlayerMovement_RightDirection_Success()
        {
            Game.ResetGame();
            int initialPosX = Game.pacman.GetPosX();
            Game.GetKey = () => ConsoleKey.RightArrow;

            Game.ReadUserKey();
            Game.PlayerMovement();
            int newPosX = Game.pacman.GetPosX();

            ClassicAssert.AreEqual(initialPosX + 1, newPosX);
        }

        [Test]
        public void CheckIfNoLives_NoLives_GameOver()
        {
            Game.ResetGame();
            Game.pacman.LoseLife();
            Game.pacman.LoseLife();

            Game.CheckIfNoLives();

            ClassicAssert.IsFalse(Game.continueLoop);
        }

        [Test]
        public void CheckScore_Score684_WinGame()
        {
            Game.ResetGame();
            Game.pacman.ResetScore();
            Game.pacman.IncreaseScore(684);

            Game.CheckScore();

            ClassicAssert.IsFalse(Game.continueLoop);
        }

        [Test]
        public void ResetGame_SetsPacmanToInitialState()
        {
            Game.pacman.LoseLife();
            Game.pacman.IncreaseScore(100);

            Game.ResetGame();
            int initialPosX = Game.pacman.GetPosX();
            int initialPosY = Game.pacman.GetPosY();
            int lives = Game.pacman.Lives();
            int score = Game.pacman.Score;

            ClassicAssert.AreEqual(17, initialPosX);
            ClassicAssert.AreEqual(20, initialPosY);
            ClassicAssert.AreEqual(3, lives);
            ClassicAssert.AreEqual(0, score);
        }
    }
}
