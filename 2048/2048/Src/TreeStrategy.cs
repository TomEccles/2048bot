using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace _2048
{
    class TreeStrategy : Strategy
    {
        private PositionEvaluator evaluator;
        private int moveTimeInMs;
        private MoveNode rootNode;

        private NodeCache nodeCache;

        public TreeStrategy(PositionEvaluator evaluator, int moveTimeInMs)
        {
            this.evaluator = evaluator;
            this.moveTimeInMs = moveTimeInMs;
            nodeCache = new NodeCache();
        }

        public Direction getDirection(GameBoard board)
        {
            Stopwatch timer = Stopwatch.StartNew();
            int level = 0;
            rootNode = new MoveNode(board);

            nodeCache.clearBelow(board.sum());

            rootNode = nodeCache.getMoveNode(board);

            List<GameNode> bottomNodes = new List<GameNode>();
            bottomNodes.Add(rootNode);
            while (timer.ElapsedMilliseconds < moveTimeInMs)
            {
                level++;
                List<GameNode> newNodes = new List<GameNode>();
                foreach (GameNode node in bottomNodes)
                {
                    node.evaluateChildren(nodeCache);
                    newNodes.AddRange(node.getChildren());
                }
                bottomNodes = newNodes;
            }
            Console.WriteLine(level);
            return rootNode.bestDirection(evaluator);
        }
    }
}
