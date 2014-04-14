using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class EvaluatorStrategy : Strategy
    {
        private PositionEvaluator evaluator;

        public EvaluatorStrategy(PositionEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public Direction getDirection(GameBoard board)
        {
            double bestScore = -1;
            Direction bestDirection = Direction.None;
            foreach (Direction tryDirection in (Direction[])Enum.GetValues(typeof(Direction)))
            {
                if (tryDirection == Direction.None) continue;
                GameBoard newBoard = board.move(tryDirection);
                if (newBoard.Equals(board)) continue;
                double newScore = evaluator.score(newBoard);
                if (newScore > bestScore)
                {
                    bestScore = newScore;
                    bestDirection = tryDirection;
                }
            }
            return bestDirection;
        }
    }
}
