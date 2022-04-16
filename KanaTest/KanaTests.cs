using NUnit.Framework;
using Kana;

namespace KanaTest.UnitTests
{
    public class KanaTests
    {
        [Test]
        public void GameDoesNotStartWithLessThanTwoPlayers()
        {
            KanaGame kana = new KanaGame();

            kana.Start();

            Assert.IsFalse(kana.GetGameState(), "You should add two players at least");
        }

        [Test]
        public void GameDoesStartsWithTwoPlayers()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            Assert.IsTrue(kana.GetGameState());
        }

        [Test]
        public void GameDoesStartsWithMoreThanTwoPlayers()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");
            kana.AddPlayer("P3");

            kana.Start();

            Assert.IsTrue(kana.GetGameState());
        }

        [Test]
        public void AfterStartingGamePlayersTokenAreAtPositionOne()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var players = kana.GetPlayers();

            Assert.AreEqual(players[0].GetToken(), 1);
            Assert.AreEqual(players[1].GetToken(), 1);
        }

        [Test]
        public void PlayerTokenMovesXPositionsAtStartNotSpecial()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var player = kana.GetPlayer();
            Assert.AreEqual(player.GetToken(), 1);

            kana.Move(4, player);

            Assert.AreEqual(player.GetToken(), 5);
        }

        [Test]
        public void PlayerTokenMovesXPositionsAfterFirstPositioNotSpecial()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var player = kana.GetPlayer();
            Assert.AreEqual(player.GetToken(), 1);

            kana.Move(4, player);

            Assert.AreEqual(player.GetToken(), 5);

            kana.Move(1, player);

            Assert.AreEqual(player.GetToken(), 6);
        }

        [Test]
        public void PlayerTokenMovesXPositionsAtStartSpecialSquare()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var player = kana.GetPlayer();
            Assert.AreEqual(player.GetToken(), 1);

            kana.Move(1, player);

            Assert.AreEqual(player.GetToken(), 38);
        }

        [Test]
        public void PlayerTokenMovesXPositionsAfterFirstPositioSpecialSquare()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var player = kana.GetPlayer();
            Assert.AreEqual(player.GetToken(), 1);

            kana.Move(4, player);

            Assert.AreEqual(player.GetToken(), 5);

            kana.Move(2, player);

            Assert.AreEqual(player.GetToken(), 14);
        }

        [Test]
        public void PlayerWinAfterReachingPosition100()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var player = kana.GetPlayer();
            Assert.AreEqual(player.GetToken(), 1);

            kana.Move(97, player);

            Assert.AreEqual(player.GetToken(), 98);

            kana.Move(2, player);

            Assert.AreEqual(player.GetToken(), 100);

            Assert.AreEqual(kana.GetWinner(), player);
        }

        [Test]
        public void PlayerDoesNotMoveIfTokenSurpasses100()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var player = kana.GetPlayer();
            Assert.AreEqual(player.GetToken(), 1);

            kana.Move(97, player);

            Assert.AreEqual(player.GetToken(), 98);

            kana.Move(3, player);

            Assert.AreEqual(player.GetToken(), 98);
        }

        [Test]
        public void PlayerMovesUsingADiceRoll()
        {
            KanaGame kana = new KanaGame();

            kana.AddPlayer("P1");
            kana.AddPlayer("P2");

            kana.Start();

            var player = kana.GetPlayer();
            var expected = 1;

            Assert.AreEqual(player.GetToken(), expected);

            var diceRoll = kana.RollDice();

            kana.Move(diceRoll, player);

            var result = expected + diceRoll + player.GetSpecialMove().Value;

            Assert.AreEqual(player.GetToken(), result);            
        }
    }
}