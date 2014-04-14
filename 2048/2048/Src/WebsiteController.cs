using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace _2048
{
    class RealController : Controller
    {
        private FirefoxDriver driver = new FirefoxDriver();

        public RealController()
        {
            String baseURL = "http://gabrielecirulli.github.io/2048/";
            driver.Navigate().GoToUrl(baseURL);
        }

        public void move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    press(Keys.ArrowDown);
                    break;
                case Direction.Up:
                    press(Keys.ArrowUp);
                    break;
                case Direction.Left:
                    press(Keys.ArrowLeft);
                    break;
                case Direction.Right:
                    press(Keys.ArrowRight);
                    break;
                default:
                    throw new Exception("Invalid direction for move");
            }
        }

        public void left()
        {
            press(Keys.ArrowLeft);
        }

        public void right()
        {
            press(Keys.ArrowRight);
        }

        public void up()
        {
            press(Keys.ArrowUp);
        }

        public void down()
        {
            press(Keys.ArrowDown);
        }

        private void press(String key)
        {           
            driver.FindElementsByClassName("tile-container").ElementAt(0).SendKeys(key);
            System.Threading.Thread.Sleep(100);
            try
            {
                driver.FindElements(By.ClassName("game-message-game-won"));
                driver.FindElementsByClassName("keep-playing-button").ElementAtOrDefault(0).Click();
            }
            catch(Exception)
            {
            }
        }

        public GameBoard getBoardWhenSumExceeds(int bar)
        {
            while (true)
            {
                GameBoard board = getBoard();
                if (board.sum() > bar) return board;
                else System.Threading.Thread.Sleep(100);
            }
        }

        public GameBoard getBoard()
        {
            var squares = driver.FindElementsByClassName("tile-container").ElementAt(0)
                                           .FindElements(By.TagName("div"))
                                           .Select(square => new Square(square.GetAttribute("class")))
                                           .Where(x => x.number != 0)
                                           .ToList();
            var grid = new int[4, 4];
            foreach (Square square in squares)
            {
                grid[square.x - 1, square.y - 1] = square.number;
            }
            return new GameBoard(grid);
        }
    }
}
