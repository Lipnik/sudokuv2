using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.Model
{
    public static class PuzzleFactory
    {
        public static Puzzle Create(int difficulty)
        {
            var rows = new List<FillGroup>(9);
            var columns = new List<FillGroup>(9);
            var groups = new List<FillGroup>(9);

            //container inicializer
            for (int i = 1; i <= 9; i++)
            {
                rows.Add(new FillGroup(i));
                columns.Add(new FillGroup(i));
                groups.Add(new FillGroup(i));
            }

            var puzzle = new Puzzle();

            //assign cells
            for (int i = 1; i <= 81; i++)
            {
                //container ids
                var rowId = (i - 1) / 9 + 1;
                var columnId = (i - 1) % 9+1;
                var groupId = (rowId - 1) / 3 * 3 + (columnId - 1) / 3 + 1;
                //add cell to puzzle
                puzzle.AddCell(new Cell(i,rows.Single(r=>r.id==rowId),columns.Single(c=>c.id==columnId),groups.Single(g=>g.id==groupId)));
            }

            //generate puzzle, start from index 1
            PuzzleGenerator.Generate(puzzle);

            #region Select Fixed Cells
            var fixedCellIndices = new List<int>();
            var rand = new Random();
            while (fixedCellIndices.Count < 81 - difficulty * 9)
            {
                var randIndex = rand.Next(1,82);
                if (fixedCellIndices.Contains(randIndex))
                {
                    continue;
                }
                else
                {
                    fixedCellIndices.Add(randIndex);
                }
            }

            foreach (var fixedCellIndex in fixedCellIndices)
            {
                puzzle.FixateCell(fixedCellIndex);
            }

            #endregion

            //set free cells to 0
            var freeCells = puzzle.FreeCells;
            foreach (var cell in freeCells)
            {
                cell.value = 0;
            }

            return puzzle;
        }
    }
}
