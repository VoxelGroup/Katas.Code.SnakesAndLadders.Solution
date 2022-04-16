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

        public void Start()
        {
            if (Players.Count > 1)
            {
                IsStarted = true;
                PlayerTurn = 0;
            }            
        }

        public bool GetGameState()
        {
            return IsStarted;
        }

        private void GenerateBoard()
        {
            for (int i = 1; i <= BoardSize; i++)
            {
                bool hasSpecial = false;

                for (int e = 0; e < SpecialSquares.Count; e++)
                {
                    if (SpecialSquares[e].GetToken() == i)
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

        public void AddPlayer(string playerName)
        {
            if (!IsStarted)
            {
                Players.Add(new Player(playerName, Board[0]));
            }
        }

        public int? RollDice()
        {
            if (IsStarted)
            {
                var random = new Random();

                return random.Next(1, 6);
            }
            return null;
        }

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

        private void Stop()
        {
            IsStarted = false;            
        }

        public Player GetWinner()
        {
            if (Winner != null)
            {
                return Winner;
            }
            return null;
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }

        public Player GetPlayer()
        {
            return Players[PlayerTurn];
        }
    }
}
