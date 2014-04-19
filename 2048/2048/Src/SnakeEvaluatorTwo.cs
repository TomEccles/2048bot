using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class SnakeEvaluatorTwo : PositionEvaluator
    {
        private List<Tuple<int, int>> snakeOrder = new List<Tuple<int, int>>();

        public SnakeEvaluatorTwo()
        {
            for (int i = 0; i < 4; i++)
            {
                snakeOrder.Add(new Tuple<int, int> (0, i));
            }
            for (int i = 0; i < 4; i++)
            {
                snakeOrder.Add(new Tuple<int, int>(1, 3 - i));
            }
            for (int i = 0; i < 4; i++)
            {
                snakeOrder.Add(new Tuple<int, int>(2, i));
            }
            for (int i = 0; i < 4; i++)
            {
                snakeOrder.Add(new Tuple<int, int>(3, 3 - i));
            }
        }

        public double score(GameBoard board)
        {
            double score = 1;
            double last = 10000000;
            int i;
            for (i = 0; i < 16; i++)
            {
                var square = snakeOrder.ElementAt(i);
                double current = board.squares[square.Item1, square.Item2];
                if (current <= last && current > 0)
                {
                    score += current;
                    if (i == 4 && current == last)
                    {
                        i++;
                        break;
                    }
                    last = current;
                }
                else if (i == 0 || current == 0)
                {
                    break;
                }
                else //we are blocking the snake. If there's another way to increase the tail, we should award points for that
                {
                    var tailSquare = snakeOrder.ElementAt(i - 1);
                    for (int j = i; j < 16; j++)
                    {
                        var trySquare = snakeOrder.ElementAt(j);
                        if (Math.Abs(trySquare.Item1 - tailSquare.Item1) + Math.Abs(trySquare.Item2 - tailSquare.Item2) == 1)
                        {
                            int adjValue = board.squares[trySquare.Item1, trySquare.Item2];
                            if (adjValue > 0 && adjValue <= last)
                            {
                                score += adjValue/2;
                            }
                        }
                    }
                    break;
                }
            }
            int badScore = 0;
            for (; i < 16; i++)
            {
                var square = snakeOrder.ElementAt(i);
                badScore += board.squares[square.Item1, square.Item2] * 8;
            }
            score -= Math.Min(badScore, score / 8);
            return score;
        }
    }
}
