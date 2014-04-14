using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class UpLeftStrategy : Strategy
    {
        public Direction getDirection(GameBoard board)
        {
            if (!board.Equals(board.move(Direction.Up)))
            {
                return Direction.Up;
            }
            else if (!board.Equals(board.move(Direction.Left)))
            {
                return Direction.Left;
            }
            else if (!board.Equals(board.move(Direction.Right)))
            {
                return Direction.Right;
            }
            else if (!board.Equals(board.move(Direction.Down)))
            {
                return Direction.Down;
            }
            else
            {
                return Direction.None;
            }
        }
    }
}
