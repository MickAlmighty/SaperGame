using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaperLib
{
    public class GameManager
    {
        public SaperGrid Grid { get; set; }
        public bool GameOver{ get; private set; }
        public GameManager()
        {
            GameOver = false;
        }

        public void CreateBoard(int rows, int columns)
        {
            Grid = new SaperGrid(rows, columns);
        }

        public void StartGame()
        {
            Grid = new SaperGrid();
            System.Console.WriteLine("Starting new saper game!");
        }

        public void UnveilCell(int x, int y)
        {

            var cell = Grid.GetCell(x, y);
            if (cell.IsVisible)
                return;

            if (cell.HasBomb)
            {
                GameOver = true;
                cell.IsVisible = true;
                return;
            }
            
            if (!cell.HasBomb)
            {
                if (Grid.GetAdjacentMinesForCell(x, y) != 0)
                {
                    cell.IsVisible = true;
                    return;
                }

                //from here Grid.GetAdjacentMinesForCell(x, y) == 0
                cell.IsVisible = true;

                for (int i = x - 1; i <= x + 1; ++i)
                {
                    for (int j = y - 1; j <= y + 1; ++j)
                    {
                        if (!Grid.AreIndicesValid(i, j))
                        {
                            continue;
                        }

                        UnveilCell(i, j);
                    }
                }
            }

        }
    }
}
