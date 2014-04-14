using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class AppearNode : GameNode
    {
        private List<Tuple<MoveNode, double>> children;
        Boolean childrenEvaluated;

        private GameBoard board;
        private PositionEvaluator evaluator;
        public Direction lastDirection;

        public AppearNode(GameBoard board, Direction moveDirection, PositionEvaluator evaluator)
        {
            this.board = board.move(moveDirection);
            this.lastDirection = moveDirection;
            this.evaluator = evaluator;
        }

        private Tuple<MoveNode, double> getChild(Square square)
        {
            MoveNode node = new MoveNode(board, square, evaluator);
            if (square.number == 2) return Tuple.Create(node, 0.9);
            else return Tuple.Create(node, 0.1);
        }

        public List<GameNode> getChildren()
        {   
            return children.Select(child => child.Item1).Cast<GameNode>().ToList();
        }

        public void calculateChildren()
        {
            childrenEvaluated = true;
            List<Square> appearSquares = board.GetAppearSquare();
            children = appearSquares.Select(square => getChild(square)).ToList();
        }

        public double score()
        {
            if (childrenEvaluated)
            {
                List<double> childScores = children.Select(child => child.Item1.score() * child.Item2 * 2).ToList();
                return 0.9 * childScores.Average() + 0.1 * evaluator.score(board);
            }
            else
            {
                return evaluator.score(board);
            }
        }

    }
}
