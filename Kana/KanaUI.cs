using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kana
{
    class KanaUI
    {
        private KanaGame Kana { get; }

        public KanaUI(KanaGame kana)
        {
            Kana = kana;
        }

        public void StartGame()
        {
            AddPlayers();
            Kana.Start();
            GameLoop();
        }

        private void GameLoop()
        {
            while (Kana.GetGameState())
            {
                Player player = Kana.GetPlayer();

                TurnInfo(player);
                Kana.Move(RollDice(), player);
                MoveInfo(player);
                
                if (Kana.GetWinner() != null)
                {
                    ShowWinnerInfo(Kana.GetWinner());
                }

                Console.WriteLine("---------------------------");
            }
        }

        private void ShowWinnerInfo(Player player)
        {
            Console.WriteLine($"You've won {player.GetPlayerName()}!");
        }

        private void MoveInfo(Player player)
        {
            Console.WriteLine($"You've moved to {player.GetToken()}");
        }

        private void TurnInfo(Player player)
        {
            Console.WriteLine($"It's your turn, {player.GetPlayerName()}. You're at position {player.GetToken()}");
        }

        private int RollDice()
        {
            Console.WriteLine("Press any key to roll a dice");
            Console.ReadKey();

            int? roll = Kana.RollDice();

            if (roll.HasValue)
            {
                Console.WriteLine($"\nYou rolled a {roll.Value}");
                return roll.Value;
            }
            else
            {
                Console.WriteLine("\nYou must start the game before rolling the dice");
                return -1;
            }
        }

        private void AddPlayers()
        {
            var input = "";

            do
            {
                Console.WriteLine("Write the Player name or -1 if you want to stop");
                input = Console.ReadLine();
                if (input != "-1")
                {
                    Kana.AddPlayer(input);
                }
            } while (input != "-1");
        }
    }
}
