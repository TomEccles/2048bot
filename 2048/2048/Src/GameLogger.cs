using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class GameLogger
    {
        List<GameBoard> gameLog;

        public GameLogger()
        {
            gameLog = new List<GameBoard>();
        }

        public void append(GameBoard board)
        {
            gameLog.Add(board);
        }

        public void output()
        {
            while (true)
            {
                Console.WriteLine("Enter command for logger. To print a board (0 to {0}), enter a number. To quit, press q");
                String input = Console.ReadLine();
                int result;
                if (input.Equals("q"))
                {
                    break;
                }
                else if (Int32.TryParse(input, out result))
                {
                    if (0 <= result && result < gameLog.Count) gameLog.ElementAt(result).print();
                    else Console.WriteLine("bad input");
                }
                else
                {
                    Console.WriteLine("bad input");
                }
            }
        }
    }
}
