using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sudokuv2.Controller;

namespace sudokuv2.View
{
    public partial class DiffForm : Form,IView
    {
        int difficulty;

        public static IView Create()
        {
            return new DiffForm();
        }
        
        public DiffForm()
        {
            InitializeComponent();
        }

        public object GetPresentation()
        {
            return difficulty;
        }

        public void DisableInput()
        {
            throw new NotImplementedException();
        }

        public void EnableInput()
        {
            throw new NotImplementedException();
        }

        public void HintUpdate(int value)
        {
            throw new NotImplementedException();
        }

        private void button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            difficulty = int.Parse(button.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
