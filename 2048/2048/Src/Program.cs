using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Web;
using Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace _2048
{
    class Program
    {
        public static Random rand = new Random();

        private static void playGame(Controller controller, Strategy strategy)
        {
            while (true)
            {
                try
                {
                    var board = controller.getBoard();
                    var move = strategy.getDirection(board);
                    if (move != Direction.None)
                    {
                        controller.move(move);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        static void Main(string[] args)
        {
            var controller = new RealController();
            var evaluator = new SnakeEvaluatorTwo();
            var strategy = new TreeStrategy(evaluator, 100);

            playGame(controller, strategy);
        }
    }
}
