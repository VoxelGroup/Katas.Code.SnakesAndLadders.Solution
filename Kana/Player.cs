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

        /**
         * <summary>Change the position of the token</summary>
         */
        public void Move(Square position)
        {
            Token = position;
        }

        /**
         * <summary>Returns the position of the player</summary>
         * <returns>Integer with the position of the player</returns>
         */
        public int GetToken()
        {
            return Token.GetNumber();
        }

        /**
         * <summary>Returns weather the square is special or not</summary>
         * <returns>Bool with true if the square is special or false if it isn't</returns>
         */
        public bool IsSpecialSquare()
        {
            return Token.IsSpecialSquare();
        }

        /**
         * <summary>Returns the position of the special square</summary>
         * <returns>Integer with the position or null if it's not a special square</returns>
         */
        public int? GetSpecialMove()
        {
            return Token.GetNextPos();
        }

        /**
         * <summary>Returns the name of the player</summary>
         * <returns>String with the player name</returns>
         */
        public string GetPlayerName()
        {
            return PlayerName;
        }
    }
}
