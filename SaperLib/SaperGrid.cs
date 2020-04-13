using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaperLib
{
    public class SaperGrid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        private List<List<Cell>> cells;
        private List<List<int>> adjacentMines;

        public Cell GetCell(int x, int y)
        {
            return cells[x][y];
        }

        public int GetAdjacentMinesForCell(int x, int y)
        {
            return adjacentMines[x][y];
        }
        
        public SaperGrid(int rows = 10, int columns = 10, int numberOfMines = 20)
        {
            if (numberOfMines > rows * columns)
                throw new Exception("There too many mines than there is cells in the grid!");

            Rows = rows;
            Columns = columns;

            cells = new List<List<Cell>>(rows);

            for (int i = 0; i < rows; ++i)
            {
                cells.Add(new List<Cell>());
                for (int j = 0; j < columns; ++j)
                {
                    cells[i].Add(new Cell(true, false));
                }
            }

            adjacentMines = new List<List<int>>(rows);

            for (int i = 0; i < rows; ++i)
            {
                adjacentMines.Add(new List<int>());
                for (int j = 0; j < columns; ++j)
                {
                    adjacentMines[i].Add(0);
                }
            }

            int numberOfCells = Rows * Columns;
            RemoweMines(numberOfCells - numberOfMines);
            FindAdjacentMines();
        }

        ~SaperGrid()
        {
            Console.WriteLine("Saper Board has been destroyed!");
        }

        private void RemoweMines(int numberOfMinesToRemove)
        {
            DateTime dt1970 = new DateTime(1970, 1, 1);
            DateTime current = DateTime.Now;//DateTime.UtcNow for unix timestamp
            TimeSpan span = current - dt1970;

            Random rowRandom = new Random((int)span.TotalMilliseconds);
            Random columnRandom = new Random();

            while (numberOfMinesToRemove != 0)
            {
                int x = rowRandom.Next(Rows);
                int y = columnRandom.Next(Columns);
                if (cells[x][y].HasBomb)
                {
                    cells[x][y].HasBomb = false;
                    --numberOfMinesToRemove;
                }
            }
        }

        private void FindAdjacentMines()
        {
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    if (GetCell(i, j).HasBomb)
                    {
                        adjacentMines[i][j] = 0;
                    }
                    else
                    {
                        int mines = FindAdjacentCellsWithMinesForCell(i, j);
                        adjacentMines[i][j] = mines;
                    }
                }
            }
        }

        private int FindAdjacentCellsWithMinesForCell(int x, int y)
        {
            int adjacentMineCounter = 0;

            for (int i = x - 1; i <= x + 1; ++i)
            {
                for (int j = y - 1; j <= y + 1; ++j)
                {
                    if (AreIndicesValid(i, j))
                    {
                        var cell = GetCell(i, j);
                        if (cell.HasBomb)
                        {
                            ++adjacentMineCounter;
                        }
                    }
                }
            }

            return adjacentMineCounter;
        }

        public bool AreIndicesValid(int x, int y)
        {
            if (x < 0 || x >= Rows)
                return false;
            if (y < 0 || y >= Columns)
                return false;

            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var row in cells)
            {
                foreach (var ceil in row)
                {
                    if (ceil.HasBomb)
                    {
                        builder.Append(" +");
                    }
                    else
                    {
                        builder.Append(" -");
                    }
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
