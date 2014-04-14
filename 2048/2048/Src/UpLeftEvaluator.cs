using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class UpLeftEvaluator : PositionEvaluator
    {
        public double score(GameBoard board)
        {
            double score = 100;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board.Squares[i, j] > 0) score -= i + j;
                }
            }
            return score;
        }
    }
}
