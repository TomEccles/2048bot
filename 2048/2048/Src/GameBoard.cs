using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace _2048
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        None
    }

    class GameBoard
    {
        public int[,] squares = new int[4, 4];

        public GameBoard(int[,] squares)
        {
            this.squares = squares;
        }

        public GameBoard move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return left();
                case Direction.Right:
                    return right();
                case Direction.Up:
                    return up();
                case Direction.Down:
                    return down();
                default:
                    throw new Exception("invalid direction");
            }
        }

        private GameBoard up()
        {
            var newSquares = new int[4, 4];
            
            for (int i = 0; i < 4; i++)
            {
                var oldColumn = ArrayHelper.getColumn(squares, i);
                var newColumn = ArrayHelper.collapseToFirst(oldColumn);
                ArrayHelper.putColumn(newColumn, newSquares, i);
            }
            return new GameBoard(newSquares);
        }

        private GameBoard down()
        {
            int[,] newSquares = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                var oldColumn = ArrayHelper.getColumn(squares, i);
                var newColumn = ArrayHelper.collapseToLast(oldColumn);
                ArrayHelper.putColumn(newColumn, newSquares, i);
            }
            return new GameBoard(newSquares);
        }

        private GameBoard left()
        {
            var newSquares = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                var oldRow = ArrayHelper.getRow(squares, i);
                var newRow = ArrayHelper.collapseToFirst(oldRow);
                ArrayHelper.putRow(newRow, newSquares, i);
            }
            return new GameBoard(newSquares);
        }

        private GameBoard right()
        {
            var newSquares = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                var oldRow = ArrayHelper.getRow(squares, i);
                var newRow = ArrayHelper.collapseToLast(oldRow);
                ArrayHelper.putRow(newRow, newSquares, i);
            }
            return new GameBoard(newSquares);
        }

        public override Boolean Equals(Object other)
        {
            GameBoard otherBoard = other as GameBoard;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (squares[i, j] != otherBoard.squares[i, j]) return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hc = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    hc = hc*7 + squares[i, j];
                }
            }
            return hc;
        }

        internal GameBoard randomNewTile()
        {
            var empties = new List<Tuple<int, int>>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (squares[i, j] == 0)
                    {
                        empties.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            int index = Program.rand.Next() % empties.Count();
            Tuple<int, int> square = empties.ElementAt(index);
            int[,] NewSquares = new int[4, 4];
            Array.Copy(squares, NewSquares, 16);
            NewSquares[square.Item1, square.Item2] = Program.rand.Next() % 10 == 0 ? 4 : 2;
            return new GameBoard(NewSquares);
        }

        public double score()
        {
            double total = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (squares[i, j] > 0) total += squares[i, j] * Math.Log(squares[i, j]) / Math.Log(2);
                }
            }
            return total;
        }

        internal List<Square> GetAppearSquare()
        {
            var empties = new List<Square>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (squares[i, j] == 0)
                    {
                        empties.Add(new Square(i, j, 2));
                        empties.Add(new Square(i, j, 4));
                    }
                }
            }
            return empties;
        }

        internal GameBoard withNewSquare(Square square)
        {
            int[,] NewSquares = new int[4, 4];
            Array.Copy(squares, NewSquares, 16);
            NewSquares[square.x, square.y] = square.number;
            return new GameBoard(NewSquares);
        }

        internal int sum()
        {
            int total = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (squares[i, j] > 0) total += squares[i, j];
                }
            }
            return total;
        }

        internal void print()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0} ", squares[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
