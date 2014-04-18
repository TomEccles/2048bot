using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using _2048.Src;

namespace _2048
{
    class TreeStrategy : Strategy
    {
        private PositionEvaluator evaluator;
        private int moveTimeInMs;
        private MoveNode rootNode;

        private NodeCache<MoveNode> moveNodeCache;
        private NodeCache<AppearNode> appearNodeCache;

        public TreeStrategy(PositionEvaluator evaluator, int moveTimeInMs)
        {
            this.evaluator = evaluator;
            this.moveTimeInMs = moveTimeInMs;
            moveNodeCache = new NodeCache<MoveNode>(evaluator);
            appearNodeCache = new NodeCache<AppearNode>(evaluator);
        }

        public Direction getDirection(GameBoard board)
        {
            Stopwatch timer = Stopwatch.StartNew();
            int level = 0;
            rootNode = new MoveNode(board, evaluator);

            moveNodeCache.clearBelow(board.sum());
            appearNodeCache.clearBelow(board.sum());

            rootNode = moveNodeCache.getNode(board);

            List<GameNode> bottomNodes = new List<GameNode>();
            bottomNodes.Add(rootNode);
            while (timer.ElapsedMilliseconds < moveTimeInMs)
            {
                level++;
                List<GameNode> newNodes = new List<GameNode>();
                foreach (GameNode node in bottomNodes)
                {
                    node.calculateChildren();
                    newNodes.AddRange(node.getChildren());
                }
                bottomNodes = newNodes;
            }
            Console.WriteLine(level);
            return rootNode.bestDirection();
        }
    }
}
