using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.Model
{
    [Serializable]
    public class Puzzle
    {
        public Puzzle()
        {
            puzzle = new List<Cell>(81);
        }

        public void AddCell(Cell cell)
        {
            puzzle.Add(cell);
        }

        /// <summary>
        /// Sets cell at index to value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns>true if valid set, false if not</returns>
        public bool SetValue(int value, int index)
        {
            var cell = puzzle.Single(c => c.id == index);
            if (cell.Validate(value))
            {
                cell.value = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveValue(int index)
        {
            var cell = puzzle.Single(c => c.id == index);
            cell.value = 0;
        }

        public void FixateCell(int index)
        {
            var cell = puzzle.Single(c => c.id == index);
            cell.fixated = true;
        }

        public void SetErrors(int value, int index)
        {
            var cell = puzzle.Single(c => c.id == index);
            var errorCells = cell.ErrorCells(value);
            foreach (var errorCell in errorCells)
            {
                errorCell.error = true;
            }
        }

        public void ClearErrors()
        {
            var errors = puzzle.Where(c => c.error);
            foreach (var cell in errors)
            {
                cell.error = false;
            }
        }

        public bool CellFixated(int index)
        {
            var cell = puzzle.Single(c => c.id == index);
            return cell.fixated;
        }

        public bool IsSolved()
        {
            var emptyCells = puzzle.Where(c => c.value == 0);
            if (emptyCells.Count()==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AllEmptyCells()
        {
            if (FreeFilledCells.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Cell> FreeCells
        {
            get
            {
                return puzzle.Where(c => c.fixated == false).ToList();
            }
        }

        private List<Cell> FreeFilledCells
        {
            get
            {
                return FreeCells.Where(c => c.value > 0).ToList();
            }
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return puzzle.GetEnumerator();
        }

        private List<Cell> puzzle;

        private List<Cell> solvedPuzzle;

        public int GetHint(int index)
        {
            return solvedPuzzle.Single(c => c.id == index).value;
        }

        public void SetSolved()
        {
            List<Cell> copy = new List<Cell>();
            foreach (var cell in puzzle)
            {
                copy.Add(new Cell(cell.id, cell.value));
            }
            solvedPuzzle = copy;
        }

        public void ClearInputs()
        {
            foreach (var cell in FreeCells)
            {
                cell.value = 0;
            }
        }
    }
}
