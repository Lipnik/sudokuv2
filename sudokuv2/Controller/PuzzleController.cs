using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudokuv2.Model;
using System.Windows.Forms;
using System.Drawing;
using sudokuv2.View;

namespace sudokuv2.Controller
{
    public class PuzzleController
    {
        IView form;
        Puzzle puzzle;

        public PuzzleController(IView form)
        {
            this.form = form;
        }

        private void LoadPuzzle()
        {            
            var puzzleView = form.GetPresentation() as List<Control>;       
            foreach (var cell in puzzle)
            {
                var cellView = puzzleView.Single(c => c.Name == "label" + cell.id.ToString());
                cellView.Text = cell.value.ToString();

                #region Coloring
                //color by atribubte
                if (cell.error)
                {
                    cellView.ForeColor = Color.Red;
                }
                else if (cell.fixated)
                {
                    cellView.ForeColor = Color.Blue;
                }             
                else
                {
                    //color by value
                    if (cell.value == 0)
                    {
                        cellView.ForeColor = Control.DefaultBackColor;
                    }
                    else
                    {
                        cellView.ForeColor = Control.DefaultForeColor;
                    }
                } 
                #endregion
            }
        }

        public void NewGame()
        {
            var diffForm = ViewFactory.GetView("diff");
            var ok=diffForm.ShowDialog();
            int difficulty;
            if (ok==DialogResult.OK)
            {
                difficulty = (int)diffForm.GetPresentation();
            }
            else
            {
                difficulty = 0;
            }

            if (difficulty>0)
            {
                puzzle = PuzzleFactory.Create(difficulty);
                LoadPuzzle();
                form.EnableInput();  
            }               
        }

        public void InsertValue(int value, int index)
        {
            puzzle.ClearErrors();

            bool errorMessage = false;
            bool solved = false;

            if (!puzzle.CellFixated(index))
            {
                if (value > 0)
                {
                    if (!puzzle.SetValue(value, index))
                    {
                        puzzle.SetErrors(value, index);
                        errorMessage = true;
                    }
                    else
                    {
                        solved = puzzle.IsSolved();
                    }
                }
                else
                {
                    puzzle.RemoveValue(index);
                }

                LoadPuzzle();

                if (errorMessage)
                {
                    MessageBox.Show("Invalid move!");
                }
                if (solved)
                {
                    form.DisableInput();
                    MessageBox.Show("Solved!");
                }
            }
        }

        public void ClearPuzzle()
        {
            if (!puzzle.AllEmptyCells())
            {
                if (MessageBox.Show("Clear all entries?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    puzzle.ClearErrors();
                    puzzle.ClearInputs();
                    LoadPuzzle();
                }      
            }
            else
            {
                MessageBox.Show("Nothing ti clear!");
            }
        }
    }
}
