using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class MoveNode : GameNode
    {
        private List<AppearNode> children;
        private GameBoard board;
        private PositionEvaluator evaluator;
        Boolean childrenEvaluated;

        public MoveNode(GameBoard board, Square square, PositionEvaluator evaluator)
        {
            this.board = board.withNewSquare(square);
            this.evaluator = evaluator;
        }

        public MoveNode(GameBoard board, PositionEvaluator evaluator)
        {
            this.board = board;
            this.evaluator = evaluator;
        }

        public List<GameNode> getChildren()
        {
            return children.Cast<GameNode>().ToList();
        }

        public void calculateChildren()
        {
            childrenEvaluated = true;
            children = new List<AppearNode>();
            foreach (Direction moveDirection in (Direction[])Enum.GetValues(typeof(Direction)))
            {
                if (moveDirection == Direction.None) continue;
                if (moveDirection == Direction.Down) continue;
                if (board.move(moveDirection).Equals(board)) continue;

                AppearNode child = new AppearNode(board, moveDirection, evaluator);
                children.Add(child);
            }
            if (!children.Any())
            {
                Direction moveDirection = Direction.Down;
                if (!board.move(moveDirection).Equals(board))
                {
                    AppearNode child = new AppearNode(board, moveDirection, evaluator);
                    children.Add(child);
                }
            }
        }

        public double score()
        {
            if (childrenEvaluated)
            {
                if (children.Any())
                {
                    return children.Select(child => child.score()).Max();
                }
                else return 0;
            }
            else
            {
                return evaluator.score(board);
            }
        }

        public Direction bestDirection()
        {
            if (childrenEvaluated)
            {
                if (children.Any())
                {
                    return children.OrderByDescending(child => child.score()).ElementAt(0).lastDirection;
                }
                return Direction.None;
            }
            else
            {
                throw new Exception("Can't get best direction - no children evaluated");
            }
        }
    }
}
