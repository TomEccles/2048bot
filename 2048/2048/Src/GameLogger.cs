using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace _2048
{
    public class GameLogger
    {
        List<GameBoard> gameLog;
        private Stopwatch timer;

        public GameLogger()
        {
            gameLog = new List<GameBoard>();
            timer = Stopwatch.StartNew();
        }

        public void append(GameBoard board)
        {
            gameLog.Add(board);
        }

        public void output()
        {
            Console.WriteLine("{0} nodes evaluated in {1} milliseconds; average time {2}ms", NodeCache.nodesRetrieved, timer.ElapsedMilliseconds, (double)timer.ElapsedMilliseconds/NodeCache.nodesRetrieved);
            int lastBoard = 0;
            while (true)
            {
                Console.WriteLine("Enter command for logger. To print a board (0 to {0}), enter a number. n for next, p for previous. To quit, press q", gameLog.Count - 1);
                String input = Console.ReadLine();
                int nextBoard = -1;
                if (input.Equals("q"))
                {
                    break;
                }
                else if (input.Equals("p"))
                {
                    nextBoard = lastBoard - 1;
                }
                else if (input.Equals("n"))
                {
                    nextBoard = lastBoard + 1;
                }
                else
                {
                    Int32.TryParse(input, out nextBoard);                 
                }

                if (0 <= nextBoard && nextBoard < gameLog.Count)
                {
                    gameLog.ElementAt(nextBoard).print();
                    lastBoard = nextBoard;
                }
                else Console.WriteLine("bad input");

            }
        }
    }
}
