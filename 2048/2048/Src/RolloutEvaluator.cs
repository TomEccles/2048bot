using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class RolloutEvaluator : PositionEvaluator
    {
        private Strategy strategy;
        private int rollouts;

        public RollerOuter(Strategy strategy, int rollouts)
        {
            this.strategy = strategy;
            this.rollouts = rollouts;
        }

        public double score(GameBoard board)
        {
            int moves = 0;
            for (int i = 0; i < rollouts; i++)
            {
                MockController controller = new MockController(board);
                while (true)
                {
                    moves++;
                    var moveBoard = controller.getBoard();
                    Direction bestDirection = strategy.getDirection(moveBoard);
                    if (bestDirection != Direction.None)
                    {
                        controller.move(bestDirection);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return (double)moves / (double)rollouts;
        }
    }
}
