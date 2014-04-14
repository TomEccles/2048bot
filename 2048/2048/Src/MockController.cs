using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class MockController : Controller
    {
        private GameBoard board;

        public MockController()
        {
            board = new GameBoard(new int[4, 4]);
            board = board.randomNewTile();
            board = board.randomNewTile();
        }

        public MockController(GameBoard inputBoard)
        {
            board = inputBoard;
        }

        public void move(Direction direction)
        {
            board = board.move(direction);
            board = board.randomNewTile();
        }

        public GameBoard getBoard()
        {
            return board;
        }

        internal void printGame()
        {
            board.print();
        }
    }
}
