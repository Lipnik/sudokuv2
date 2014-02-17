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
    public partial class MainForm : Form,IView
    {
        PuzzleController controller;
        int input;

        public MainForm()
        {
            controller = new PuzzleController(this);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            input = 1;
            button1.BackColor = Color.CornflowerBlue;
        }

        public object GetPresentation()
        {
            List<Control> puzzleView = new List<Control>();
            for (int i = 1; i <=81; i++)
            {
                puzzleView.Add(this.Controls.Find("label"+i.ToString(), false)[0]);
            }
            return puzzleView;
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            controller.NewGame();          
        }

        private void button_Click(object sender, EventArgs e)
        {
            var selected = sender as Button;
            var prevSelected = this.Controls.Find("button" + input.ToString(), false)[0] as Button;
            if (selected!=prevSelected)
            {
                prevSelected.BackColor = Control.DefaultBackColor;
                selected.BackColor = Color.CornflowerBlue;
                try
                {
                    input = int.Parse(selected.Text);
                }
                catch (Exception)
                {
                    input = 0;
                }
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            var clicked = sender as Label;
            int index = int.Parse(clicked.Name.Split('l')[2]);
            controller.InsertValue(input, index);
        }

        public void EnableInput()
        {
            for (int i = 1; i <= 81; i++)
            {
                this.Controls.Find("label" + i.ToString(), false)[0].Click -= label_Click;
                this.Controls.Find("label" + i.ToString(), false)[0].Click += label_Click;
            }

            Clear.Enabled = true;
        }

        public void DisableInput()
        {
            for (int i = 1; i <= 81; i++)
            {
                this.Controls.Find("label" + i.ToString(), false)[0].Click -= label_Click;
            }

            Clear.Enabled = false;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            controller.ClearPuzzle();
        }
    }
}
