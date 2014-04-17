using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    public class MoveNode : GameNode
    {
        public List<Tuple<Direction, AppearNode>> children;

        public MoveNode(GameBoard board)
        {
            this.board = board;
        }

        public override List<GameNode> getChildren()
        {
            return children.Select(child => child.Item2).Cast<GameNode>().ToList();
        }

        public override void evaluateChildren(NodeCache nodeCache)
        {
            if (!childrenEvaluated)
            {
                childrenEvaluated = true;
                children = new List<Tuple<Direction, AppearNode>>();
                foreach (Direction moveDirection in (Direction[])Enum.GetValues(typeof(Direction)))
                {
                    if (moveDirection == Direction.None) continue;
                    if (moveDirection == Direction.Down) continue;
                    if (board.move(moveDirection).Equals(board)) continue;

                    GameBoard newBoard = board.move(moveDirection);
                    AppearNode child = nodeCache.getAppearNode(newBoard);
                    children.Add(Tuple.Create(moveDirection, child));
                }
                if (!children.Any())
                {
                    Direction moveDirection = Direction.Down;
                    if (!board.move(moveDirection).Equals(board))
                    {
                        GameBoard newBoard = board.move(moveDirection);
                        AppearNode child = nodeCache.getAppearNode(newBoard);
                        children.Add(Tuple.Create(moveDirection, child));
                    }
                }
            }
        }

        public Direction bestDirection(PositionEvaluator evaluator)
        {
            if (childrenEvaluated)
            {
                if (children.Any())
                {
                    return children.OrderByDescending(child => child.Item2.score(evaluator)).ElementAt(0).Item1;
                }
                return Direction.None;
            }
            else
            {
                throw new Exception("Can't get best direction - no children evaluated");
            }
        }

        protected override double evaluateScore(PositionEvaluator evaluator)
        {
            scoreEvaluated = true;
            if (childrenEvaluated)
            {
                if (children.Any())
                {
                    calculatedScore = children.Select(child => child.Item2.score(evaluator)).Max();
                }
                else
                {
                    calculatedScore = 0;
                }
            }
            else
            {
                calculatedScore = evaluator.score(board);
            }
            return calculatedScore;
        }
    }
}
