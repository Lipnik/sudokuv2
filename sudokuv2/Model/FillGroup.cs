using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.Model
{
    public class FillGroup
    {
        public FillGroup(int id)
        {
            this.id = id;
            Cells = new List<Cell>(9);
        }

        public bool Validate(int value, int cellId)
        {
            if (Cells.Where(c=>c.id!=cellId).Select(c=>c.value).Contains(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddCell(Cell cell)
        {
            Cells.Add(cell);
        }

        public Cell Where(int value)
        {
            try
            {
                var cell = Cells.Single(c => c.value == value);
                return cell;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int id{get; private set;}
        private List<Cell> Cells;
    }
}
