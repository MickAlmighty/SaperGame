using System;
using System.Text;
using SaperLib;

namespace SaperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GameManager gameManager = new GameManager();
            gameManager.CreateBoard(10, 10);

            while (!gameManager.GameOver)
            {
                ShowGrid(gameManager.Grid);
                int x = 0;
                int y = 0;
                while (true)
                {
                    Console.WriteLine("Pass X:");
                    var potentialX = Console.ReadLine();
                    try
                    {
                        x = int.Parse(potentialX); //potentialNumer;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        continue;
                    }
                }
                while (true)
                {
                    Console.WriteLine("Pass Y:");
                    var potentialNumer = Console.ReadLine();
                    try
                    {
                        y = int.Parse(potentialNumer); //potentialNumer;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        continue;
                    }
                }

                gameManager.UnveilCell(x, y);
            }

            ShowGrid(gameManager.Grid);
            Console.WriteLine("Game Over!");

            System.GC.Collect();
        }

        static void ShowGrid(SaperGrid grid)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < grid.Rows; ++i)
            {
                for (int j = 0; j < grid.Columns; ++j)
                {
                    var cell = grid.GetCell(i, j);
                    if (!cell.IsVisible)
                    {
                        builder.Append(" O");
                    }
                    else
                    {
                        var adjacentMines = grid.GetAdjacentMinesForCell(i, j);
                        if (adjacentMines != 0)
                        {
                            builder.Append(" ");
                            builder.Append(adjacentMines);
                            continue;
                        }
                        else if (cell.HasBomb)
                        {
                            builder.Append(" +");
                        }
                        else
                        {
                            builder.Append(" -");
                        }
                    }
                }
                builder.AppendLine();
            }
            Console.Write(builder.ToString());
        }
    }
}
