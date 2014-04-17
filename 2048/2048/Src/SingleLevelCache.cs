using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048.Src
{
    class SingleLevelCache
    {
        private Dictionary<GameBoard, MoveNode> moveNodes;
        private Dictionary<GameBoard, AppearNode> appearNodes;

        public SingleLevelCache()
        {
            moveNodes = new Dictionary<GameBoard, MoveNode>();
            appearNodes = new Dictionary<GameBoard, AppearNode>();
        }

        internal MoveNode getMoveNode(GameBoard board)
        {
            MoveNode node;
            if (moveNodes.ContainsKey(board))
            {
                node = moveNodes[board];
            }
            else
            {
                node = new MoveNode(board);
                moveNodes.Add(board, node);
            }
            node.clearScore();
            return node;
        }

        internal AppearNode getAppearNode(GameBoard board)
        {
            AppearNode node;
            if (appearNodes.ContainsKey(board))
            {
                node =  appearNodes[board];
            }
            else
            {
                node = new AppearNode(board);
                appearNodes.Add(board, node);
            }
            node.clearScore();
            return node;
        }
    }
}
