using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2048.Src;

namespace _2048
{
    public class NodeCache
    {
        private Dictionary<int, SingleLevelCache> sumToCache;
        public static int nodesRetrieved = 0;

        public NodeCache()
        {
            nodesRetrieved = 0;
            sumToCache = new Dictionary<int,SingleLevelCache>();
        }

        internal void clearBelow(int threshhold)
        {
            List<int> smallKeys = new List<int>();
            foreach (int level in sumToCache.Keys)
            {
                if (level < threshhold) smallKeys.Add(level);
            }
            foreach (int key in smallKeys)
            {
                sumToCache.Remove(key);
            }
        }

        internal MoveNode getMoveNode(GameBoard board)
        {
            nodesRetrieved++;
            int sum = board.sum();
            if (!sumToCache.ContainsKey(sum))
            {
                sumToCache.Add(sum, new SingleLevelCache());
            }
            return sumToCache[sum].getMoveNode(board);
        }

        internal AppearNode getAppearNode(GameBoard board)
        {
            nodesRetrieved++;
            int sum = board.sum();
            if (!sumToCache.ContainsKey(sum))
            {
                sumToCache.Add(sum, new SingleLevelCache());
            }
            return sumToCache[sum].getAppearNode(board);
        }
    }
}
