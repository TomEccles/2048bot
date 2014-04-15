using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using _2048;

namespace _2048Test
{
    [TestFixture]
    public class GameBoardTests
    {
        [Test]
        public void compressUp()
        {
            GameBoard board = new GameBoard(new int[,]{
                                             {2, 2, 0, 0},
                                             {0, 2, 2 , 0},
                                             {0, 0, 4 , 8},
                                             {0, 0, 4 , 4}
                                             }
                                           );

            GameBoard actual = board.move(Direction.Up);
            actual.print();

            GameBoard expected = new GameBoard(new int[,]{
                                             {2, 4, 2, 8},
                                             {0, 0, 8 , 4},
                                             {0, 0, 0 , 0},
                                             {0, 0, 0 , 0}
                                             }
                                           );

            Assert.IsTrue(expected.Equals(actual));
        }

    }
}
