using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kana
{
    public class Player
    {
        private string PlayerName { get; }
        private Square Token { get; set; }

        public Player(string playerName, Square position)
        {
            PlayerName = playerName;
            Token = position;
        }

        public void Move(Square position)
        {
            Token = position;
        }

        public int GetToken()
        {
            return Token.GetToken();
        }        

        public bool IsSpecialSquare()
        {
            return Token.IsSpecialSquare();
        }

        public int? GetSpecialMove()
        {
            return Token.GetNextPos();
        }

        public string GetPlayerName()
        {
            return PlayerName;
        }
    }
}
