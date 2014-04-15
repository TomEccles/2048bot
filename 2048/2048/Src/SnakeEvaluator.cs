using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class SnakeEvaluator : PositionEvaluator
    {
        private List<Tuple<int, int>> snakeOrder = new List<Tuple<int, int>>();

        public SnakeEvaluator()
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
            double score = board.score();
            double last = 10000000;
            for (int i = 0; i < 16; i++)
            {
                var square = snakeOrder.ElementAt(i);
                double current = board.squares[square.Item2, square.Item1];
                if (current > last)
                {
                    score -= current * Math.Log(current - last) / Math.Log(2);
                }
                if (current == 0 && last > 0 && i > 0)
                {
                    score -= last * Math.Log(last) / Math.Log(2) / 2;
                }
                last = current;
            }
            return score;
        }
    }
}
