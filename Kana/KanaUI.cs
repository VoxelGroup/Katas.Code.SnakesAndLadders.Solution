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

        /**
         * <summary>Initializes a new instance of the KanaUI class</summary>
         * <param name="kana">A KanaGame instance</param>
         */
        public KanaUI(KanaGame kana)
        {
            Kana = kana;
        }

        /**
         * <summary>Is in charge of starting the game</summary>
         */
        public void StartGame()
        {
            AddPlayers();
            Kana.Start();
            GameLoop();
        }

        /**
         * <summary>Handles the game loop</summary>
         */
        private void GameLoop()
        {
            while (Kana.GetGameState())
            {
                Player player = Kana.GetPlayer();

                TurnInfo(player);

                int? roll = RollDice();
                if (roll.HasValue)
                {
                    Kana.Move(roll.Value, player);
                    MoveInfo(player);
                }
                
                if (Kana.GetWinner() != null)
                {
                    ShowWinnerInfo(Kana.GetWinner());
                }

                Console.WriteLine("---------------------------");
            }
        }

        /**
         * <summary>Prints the winner of the game</summary>
         * <param name="player">Winner Player instance</param>
         */
        private void ShowWinnerInfo(Player player)
        {
            Console.WriteLine($"You've won {player.GetPlayerName()}!");
        }

        /**
         * <summary>Prints player movement info</summary>
         * <param name="player">Player instance</param>
         */
        private void MoveInfo(Player player)
        {
            Console.WriteLine($"You've moved to {player.GetToken()}");
        }

        /**
         * <summary>Prints player turn info</summary>
         * <param name="player">Player instance</param>
         */
        private void TurnInfo(Player player)
        {
            Console.WriteLine($"It's your turn, {player.GetPlayerName()}. You're at position {player.GetToken()}");
        }

        /**
         * <summary>Handles player roll dice</summary>
         * <returns>Integer with the roll value if game has started or null if it isn't</returns>
         */
        private int? RollDice()
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
                return roll;
            }
        }

        /**
         * <summary>Is in charge of adding players to the game</summary>
         */
        private void AddPlayers()
        {           
            while(!Kana.GetGameState())
            {
                Console.WriteLine("Write the Player name or -1 if you want to stop");
                var input = Console.ReadLine();

                if (input == "-1" && Kana.CanStart())
                {
                    Kana.Start();
                }
                else if (input == "-1" && !Kana.CanStart())
                {
                    Console.WriteLine("You must add at least two players");
                }
                else
                {
                    Kana.AddPlayer(input);
                }    
            };
        }
    }
}
