using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kana
{
    public class Square
    {
        private int Number { get; set; }
        private int NextPos { get; set; }
        private bool Type { get; set; }

        public Square(int number)
        {
            Number = number;
            Type = false;
        }

        public Square(int number, int nextPos)
        {
            Number = number;
            NextPos = nextPos;
            Type = true;
        }

        /**
         * <summary>Returns the position of the square</summary>
         * <returns>Int with the square name</returns>
         */
        public int GetNumber()
        {
            return Number;
        }

        /**
         * <summary>Returns the type of the squre</summary>
         * <returns>True if it's a special square or false if not</returns>
         */
        public bool IsSpecialSquare()
        {
            return Type;
        }

        /**
         * <summary>Returns the special position of a special square</summary>
         * <returns>Int with the new posotion or null if it's not a special square</returns>
         */
        public int? GetNextPos()
        {
            return NextPos;
        }
    }
}
