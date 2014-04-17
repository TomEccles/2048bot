using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048.Src
{
    class NodeCache<T>
    {
        private PositionEvaluator evaluator;

        public NodeCache(PositionEvaluator evaluator)
        {
            // TODO: Complete member initialization
            this.evaluator = evaluator;
        }

        internal void clearBelow(int threshhold)
        {
            throw new NotImplementedException();
        }

        internal MoveNode getNode(GameBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
