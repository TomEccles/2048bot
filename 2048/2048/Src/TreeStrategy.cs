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

        public TreeStrategy(PositionEvaluator evaluator, int moveTimeInMs)
        {
            this.evaluator = evaluator;
            this.moveTimeInMs = moveTimeInMs;
        }

        public Direction getDirection(GameBoard board)
        {
            Stopwatch timer = Stopwatch.StartNew();
            int level = 0;
            rootNode = new MoveNode(board, evaluator);

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
