using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using _2048;

namespace _2048Test
{
    [TestFixture]
    public class SnakeEvaluatorTests
    {
        [Test]
        public void FullSnake()
        {
            PositionEvaluator evaluator = new SnakeEvaluatorTwo();
            GameBoard board = new GameBoard(new int[,]{
                                             {32768, 16384, 8192 , 4096},
                                             {256, 512, 1024 , 2048},
                                             {128, 64, 32 , 16},
                                             {1, 2, 4 , 8}
                                             }
                                           );
            Assert.AreEqual(65536, evaluator.score(board));
        }

        [Test]
        public void SnakeWithBlocker()
        {
            PositionEvaluator evaluator = new SnakeEvaluatorTwo();
            GameBoard board = new GameBoard(new int[,]{
                                             {64, 16, 8, 16},
                                             {0, 0, 0, 16},
                                             {0, 0, 0 , 0},
                                             {0, 0, 0 , 0}
                                             }
                                           );
            Assert.AreEqual(1 + 64 + 16 + 8 - 8*(16 + 16), evaluator.score(board));
        }


        [Test]
        public void SnakeWithEquality()
        {
            PositionEvaluator evaluator = new SnakeEvaluatorTwo();
            GameBoard board = new GameBoard(new int[,]{
                                             {64, 16, 16 , 8},
                                             {0, 4, 4 , 4},
                                             {0, 0, 0 , 0},
                                             {0, 0, 0 , 0}
                                             }
                                           );
            Assert.AreEqual(1 + 64 + 16 + 16 + 8 + 4 + 4 + 4, evaluator.score(board));
        }

        [Test]
        public void SnakeRoundTheCornerWithEquality()
        {
            PositionEvaluator evaluator = new SnakeEvaluatorTwo();
            GameBoard board = new GameBoard(new int[,]{
                                             {64, 16, 16 , 8},
                                             {0, 4, 4 , 8},
                                             {0, 0, 0 , 0},
                                             {0, 0, 0 , 0}
                                             }
                                           );
            Assert.AreEqual(1 + 64 + 16 + 16 + 8 + 8 - 8*(4 + 4), evaluator.score(board));
        }

        [Test]
        public void SnakeBlockedWithHelper()
        {
            PositionEvaluator evaluator = new SnakeEvaluatorTwo();
            GameBoard board = new GameBoard(new int[,]{
                                             {64, 16, 16 , 8},
                                             {0, 0, 8 , 4},
                                             {0, 0, 0 , 2},
                                             {0, 0, 0 , 0}
                                             }
                                           );
            Assert.AreEqual(1 + 64 + 16 + 16 + 8 + 4 + 4*2 - 8*(8+2), evaluator.score(board));
        }
    }
}
