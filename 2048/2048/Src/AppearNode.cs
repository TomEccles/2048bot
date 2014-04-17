using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    public class AppearNode : GameNode
    {
        private List<Tuple<MoveNode, double>> children;

        public Direction lastDirection;

        public AppearNode(GameBoard board)
        {
            this.board = board;
        }

        public override List<GameNode> getChildren()
        {   
            return children.Select(child => child.Item1).Cast<GameNode>().ToList();
        }

        public override void evaluateChildren(NodeCache nodeCache)
        {
            if (!childrenEvaluated)
            {
                childrenEvaluated = true;
                List<Square> appearSquares = board.GetAppearSquare();
                children = appearSquares.Select(square => getChild(nodeCache, square)).ToList();
            }
        }

        private Tuple<MoveNode, double> getChild(NodeCache nodeCache, Square square)
        {
            GameBoard newBoard = board.withNewSquare(square);
            MoveNode node = nodeCache.getMoveNode(newBoard);
            if (square.number == 2) return Tuple.Create(node, 0.9);
            else return Tuple.Create(node, 0.1);
        }

        protected override double evaluateScore(PositionEvaluator evaluator)
        {
            scoreEvaluated = true;
            if (childrenEvaluated)
            {
                List<double> childScores = children.Select(child => child.Item1.score(evaluator) * child.Item2 * 2).ToList();
                calculatedScore = 0.9 * childScores.Average() + 0.1 * evaluator.score(board);
            }
            else
            {
                calculatedScore = evaluator.score(board);
            }
            return calculatedScore;
        }
    }
}
