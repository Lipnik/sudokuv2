using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.Model
{
    public static class PuzzleGenerator
    {
        static bool complete;

        public static void Generate(Puzzle puzzle)
        {
            complete = false;
            generate(puzzle);
            SolvedPuzzle.SetPuzzle(puzzle.Copy());
        }

        private static void generate(Puzzle puzzle, int index = 1)
        {
            var possibleNumbers = Enumerable.Range(1, 9).ToList();

            while (possibleNumbers.Count > 0)
            {
                var rand = new Random();
                var randIndex = rand.Next(possibleNumbers.Count);
                var value = possibleNumbers[randIndex];
                possibleNumbers.RemoveAt(randIndex);

                if (puzzle.SetValue(value, index))
                {
                    if (index < 81)
                    {
                        generate(puzzle, index + 1);
                        if (complete)
                        {
                            break;
                        }
                    }
                    else
                    {
                        complete = true;
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
            if (!complete)
            {
                puzzle.RemoveValue(index);
            }
        }

    }
}
