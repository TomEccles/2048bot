using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using _2048;

namespace _2048.Test
{
    [TestFixture]
    class NodeCacheTests
    {
        [Test]
        public void moveNodesCached()
        {
            GameBoard board = new GameBoard(new int[,]{
                                             {2, 2, 0, 0},
                                             {0, 2, 2 , 2},
                                             {0, 0, 4 , 8},
                                             {0, 0, 4 , 4}
                                             }
                                           );
            NodeCache cache = new NodeCache();

            MoveNode node1 = cache.getMoveNode(board);
            MoveNode node2 = cache.getMoveNode(board);
            Assert.AreSame(node1, node2);
        }

        [Test]
        public void appearNodesCached()
        {
            GameBoard board = new GameBoard(new int[,]{
                                             {2, 2, 0, 0},
                                             {0, 2, 2 , 2},
                                             {0, 0, 4 , 8},
                                             {0, 0, 4 , 4}
                                             }
                                           );
            NodeCache cache = new NodeCache();

            AppearNode node1 = cache.getAppearNode(board);
            AppearNode node2 = cache.getAppearNode(board);
            Assert.AreSame(node1, node2);
        }
    }
}
