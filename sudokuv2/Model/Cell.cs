using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.Model
{
    [Serializable]
    public class Cell
    {
        public Cell(int id,FillGroup row,FillGroup column,FillGroup group)
        {
            this.id = id;
            row.AddCell(this);
            this.row = row;
            column.AddCell(this);
            this.column = column;
            group.AddCell(this);
            this.group = group;
            this.value = 0;
            this.fixated = false;
        }

        public Cell(int id, int value)
        {
            this.id = id;
            this.value = value;
        }

        public bool Validate(int value)
        {
            return row.Validate(value,this.id) && column.Validate(value,this.id) && group.Validate(value,this.id);
        }

        public List<Cell> ErrorCells(int value)
        {
            List<Cell> errorCells = new List<Cell>();

            var rowCell = row.Where(value);
            if (rowCell!=null)
            {
                errorCells.Add(rowCell);
            }
            var columnCell = column.Where(value);
            if (columnCell != null)
            {
                errorCells.Add(columnCell);
            }
            var groupCell = group.Where(value);
            if (groupCell != null)
            {
                errorCells.Add(groupCell);
            }

            return errorCells;
        }

        public int id{get;private set;}
        public int value{get;set;}
        public bool fixated { get; set; }
        public bool error { get; set; }
        private FillGroup row {get; set;}
        private FillGroup column { get; set; }
        private FillGroup group { get; set; }
    }
}
