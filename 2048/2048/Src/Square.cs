using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class Square
    {
        public int x;
        public int y;
        public int number;

        public Square(String input)
        {
            if (input.Contains("tile-position"))
            {
                int first = input.IndexOf("-");
                int second = input.IndexOf("-", first + 1);
                int third = input.IndexOf("-", second + 1);
                int fourth = input.IndexOf("-", third + 1);
                int fifth = input.IndexOf("-", fourth + 1);
                number = Convert.ToInt32(input.Substring(first + 1, second - first - 6));
                y = Convert.ToInt32(char.GetNumericValue(input.ElementAt(input.IndexOf("tile-position") + 14)));
                x = Convert.ToInt32(char.GetNumericValue(input.ElementAt(input.IndexOf("tile-position") + 16)));
            }
        }

        public Square(int i, int j, int number)
        {
            // TODO: Complete member initialization
            this.x = i;
            this.y = j;
            this.number = number;
        }
    }
}
