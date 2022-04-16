using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kana
{
    public class Square
    {
        public int Number { get; set; }
        public int NextPos { get; set; }
        public bool Type { get; set; }
        public List<Player> Players { get; set; }

        public Square(int number)
        {
            Number = number;
            Players = new List<Player>();
            Type = false;
        }

        /**
         * 0 Ladder
         * 1 Snake
         */
        public Square(int number, int nextPos)
        {
            Number = number;
            NextPos = nextPos;
            Type = true;
        }              
        
        public int GetToken()
        {
            return Number;
        }

        public bool IsSpecialSquare()
        {
            return Type;
        }

        public int? GetNextPos()
        {
            return NextPos;
        }
    }
}
