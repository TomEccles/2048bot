using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    public abstract class GameNode
    {
        protected Boolean childrenEvaluated;

        protected Boolean scoreEvaluated;
        protected double calculatedScore;

        protected GameBoard board;

        public abstract List<GameNode> getChildren();

        public abstract void evaluateChildren(NodeCache nodeCache);

        public double score(PositionEvaluator evaluator)
        {
            if (!scoreEvaluated)
            {
                evaluateScore(evaluator);
            }
            return calculatedScore;
        }

        public void clearScore()
        {
            scoreEvaluated = false;
        }

        protected abstract double evaluateScore(PositionEvaluator evalauator);
    }
}
