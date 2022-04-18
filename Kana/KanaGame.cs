using System;
using System.Collections.Generic;

namespace Kana
{
    public class KanaGame
    {
        private bool IsStarted { get; set; }        
        private int PlayerTurn { get; set;  }
        private List<Player> Players { get; set; }
        private int BoardSize { get; set; }
        private Player Winner { get; set; }
        private List<Square> Board { get; set; }
        private List<Square> SpecialSquares { get;  set; }

        public KanaGame()
        {
            IsStarted = false;
            PlayerTurn = -1;
            Players = new List<Player>();
            BoardSize = 100;
            Board = new List<Square>();
            SpecialSquares = new List<Square>
            { 
                new Square(2, 38), new Square(7, 14), new Square(8, 31),
                new Square(15, 26), new Square(16, 6), new Square(21, 42),
                new Square(28, 84), new Square(36, 44), new Square(46, 25),
                new Square(49, 11), new Square(51, 67), new Square(62, 19),
                new Square(64, 60), new Square(71, 91), new Square(74, 53),
                new Square(78, 98), new Square(87, 94), new Square(89, 68),
                new Square(92, 88), new Square(95, 75), new Square(99, 80)
            };

            GenerateBoard();
        }

        /**
         * <summary>Change the state of the game to true if there are two or more players</summary>
         */
        public void Start()
        {
            if (Players.Count > 1)
            {
                IsStarted = true;
                PlayerTurn = 0;
            }            
        }

        /**
         * <summary>Returns what's the state of the game</summary>
         * <returns>Boolean with true or false with the state of the game</returns>
         */
        public bool GetGameState()
        {
            return IsStarted;
        }

        /**
         * <summary>Generates the game board with the special squares</summary>
         */
        private void GenerateBoard()
        {
            for (int i = 1; i <= BoardSize; i++)
            {
                bool hasSpecial = false;

                for (int e = 0; e < SpecialSquares.Count; e++)
                {
                    if (SpecialSquares[e].GetNumber() == i)
                    {
                        hasSpecial = true;
                        Board.Add(SpecialSquares[e]);
                        break;
                    }      
                }

                if (!hasSpecial)
                {
                    Board.Add(new Square(i));
                }
            }
        }

        /**
         * <summary>Adds a player to the game</summary>
         * <param name="playerName">String with the name of the player</param>
         */
        public void AddPlayer(string playerName)
        {
            if (!IsStarted)
            {
                Players.Add(new Player(playerName, Board[0]));
            }
        }

        /**
         * <summary>Returns a number between 1-6 (both included)</summary>
         * <returns>An integer between 1-6 (both included) or null if game it's not started</returns>
         */
        public int? RollDice()
        {
            if (IsStarted)
            {
                var random = new Random();

                return random.Next(1, 6);
            }
            return null;
        }

        /**
         * <summary>Handles game turns</summary>
         */
        private void EndTurn()
        {
            if (IsStarted)
            {
                if (PlayerTurn + 1 >= Players.Count)
                {
                    PlayerTurn = 0;
                }
                else
                {
                    PlayerTurn += 1;
                }
            }
        }

        /**
         * <summary>Moves the player as many positions as he rolled</summary>
         * <param name="player">The player who moves</param>
         * <param name="roll">The number of positions the player moves</param>         
         */
        public void Move(int? roll, Player player)
        {
            if (IsStarted && roll.HasValue)
            {                
                if (player != null)
                {
                    var move = player.GetToken() + roll.Value;
                    
                    if (CheckValid(move) == true)
                    {                                                
                        player.Move(Board[move - 1]);
                        
                        if (player.GetToken() == 100)
                        {
                            Winner = player;
                            Stop();
                        }
                        else
                        {                            
                            if (player.IsSpecialSquare())
                            {
                                player.Move(Board[player.GetSpecialMove().Value - 1]);
                            }
                            EndTurn();
                        }
                    }
                }
            }
        }

        /**
         * <summary>Checks if the roll if valid</summary>
         * <param name="roll">Position where the player will move</param>
         * <returns>True if player can do that move, false if it's greater than the board size or null if game isn't started</returns>
         */
        private bool? CheckValid(int roll)
        {
            if (IsStarted)
            {
                if (roll <= BoardSize)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return null;
        }

        /**
         * <summary>Change the state of the game to false</summary>
         */
        private void Stop()
        {
            IsStarted = false;            
        }

        /**
         * <summary>Returns the Winner of the game</summary>
         * <returns>The player who won the game or null if there isn't a winner yet</returns>
         */
        public Player GetWinner()
        {
            if (Winner != null)
            {
                return Winner;
            }
            return null;
        }

        /**
         * <summary>Returns a list with all players</summary>
         * <returns>A list with all players</returns>
         */
        public List<Player> GetPlayers()
        {
            return Players;
        }

        /**
         * <summary>Returns the player who is playing at that moment</summary>
         * <returns>The player who is playing at that turn</returns>
         */
        public Player GetPlayer()
        {
            return Players[PlayerTurn];
        }
    }
}
