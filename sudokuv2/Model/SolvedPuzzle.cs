using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.Model
{
    public static class SolvedPuzzle
    {
        public static void SetPuzzle(List<Cell> solvedPuzzle)
        {
            puzzle=solvedPuzzle;
        }

        public static int GetValue(int index)
        {
            return puzzle.Single(c => c.id == index).value;
        }

        private static List<Cell> puzzle;
    }
}
