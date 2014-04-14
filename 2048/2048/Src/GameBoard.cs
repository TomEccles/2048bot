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
        public int[,] Squares = new int[4, 4];

        public GameBoard(int[,] NewSquares)
        {
            Squares = NewSquares;
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
            int[,] NewSquares = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                int nextSquare = 0;
                for (int k = 0; k < 4; k++)
                {
                    int found = 0;
                    for (int j = nextSquare; j < 4; j++)
                    {
                        int value = Squares[j, i];
                        if (value == 0) continue;
                        if (found != 0)
                        {
                            if (value == found)
                            {
                                found = 2 * found;
                                nextSquare = j + 1;
                                break;
                            }
                            else if (j != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            found = value;
                            nextSquare = j + 1;
                        }
                    }
                    NewSquares[k, i] = found;
                }
            }
            return new GameBoard(NewSquares);
        }

        private GameBoard down()
        {
            int[,] NewSquares = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                int nextSquare = 3;
                for (int k = 3; k >= 0; k--)
                {
                    int found = 0;
                    for (int j = nextSquare; j >= 0; j--)
                    {
                        int value = Squares[j, i];
                        if (value == 0) continue;
                        if (found != 0)
                        {
                            if (value == found)
                            {
                                found = 2 * found;
                                nextSquare = j - 1;
                                break;
                            }
                            else if (j != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            found = value;
                            nextSquare = j - 1;
                        }
                    }
                    NewSquares[k, i] = found;
                }
            }
            return new GameBoard(NewSquares);
        }

        private GameBoard left()
        {
            int[,] NewSquares = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                int nextSquare = 0;
                for (int k = 0; k < 4; k++)
                {
                    int found = 0;
                    for (int j = nextSquare; j < 4; j++)
                    {
                        int value = Squares[i, j];
                        if (value == 0) continue;
                        if (found != 0)
                        {
                            if (value == found)
                            {
                                found = 2 * found;
                                nextSquare = j + 1;
                                break;
                            }
                            else if (j != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            found = value;
                            nextSquare = j + 1;
                        }
                    }
                    NewSquares[i, k] = found;
                }
            }
            return new GameBoard(NewSquares);
        }

        private GameBoard right()
        {
            int[,] NewSquares = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                int nextSquare = 3;
                for (int k = 3; k >= 0; k--)
                {
                    int found = 0;
                    for (int j = nextSquare; j >= 0; j--)
                    {
                        int value = Squares[i, j];
                        if (value == 0) continue;
                        if (found != 0)
                        {
                            if (value == found)
                            {
                                found = 2 * found;
                                nextSquare = j - 1;
                                break;
                            }
                            else if (j != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            found = value;
                            nextSquare = j - 1;
                        }
                    }
                    NewSquares[i, k] = found;
                }
            }
            return new GameBoard(NewSquares);
        }

        public override Boolean Equals(Object other)
        {
            GameBoard otherBoard = other as GameBoard;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Squares[i, j] != otherBoard.Squares[i, j]) return false;
                }
            }
            return true;
        }

        internal GameBoard randomNewTile()
        {
            var empties = new List<Tuple<int, int>>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Squares[i, j] == 0)
                    {
                        empties.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            int index = Program.rand.Next() % empties.Count();
            Tuple<int, int> square = empties.ElementAt(index);
            int[,] NewSquares = new int[4, 4];
            Array.Copy(Squares, NewSquares, 16);
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
                    if (Squares[i, j] > 0) total += Squares[i, j] * Math.Log(Squares[i, j]) / Math.Log(2);
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
                    if (Squares[i, j] == 0)
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
            Array.Copy(Squares, NewSquares, 16);
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
                    if (Squares[i, j] > 0) total += Squares[i, j];
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
                    Console.Write("{0} ", Squares[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
