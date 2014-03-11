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
        PuzzleController pController;
        HintController hController;
        TimeController tControler;
        int input;

        public MainForm()
        {
            pController = new PuzzleController(this);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            input = 1;
            button1.BackColor = Color.CornflowerBlue;
        }

        public void HintUpdate(int value)
        {
            Button hValueButton = this.Controls.Find("button" + value.ToString(), false)[0] as Button;
            hValueButton.PerformClick();
            hController.HintUsed();
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
            Show_Stopper(New_Game);
        }

        private void button_Click(object sender, EventArgs e)
        {
            var selected = sender as Button;
            var prevSelected = this.Controls.Find("button" + input.ToString(), false)[0] as Button;
            if (selected!=prevSelected)
            {
                prevSelected.BackColor = Control.DefaultBackColor;
                selected.BackColor = Color.CornflowerBlue;
                if (selected.Text == "Delete")
                {
                    input = 0;
                }
                else if (selected.Text.Contains("Hint"))
                {
                    input = 10;
                }
                else
                {
                    input = int.Parse(selected.Text);
                }
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            var clicked = sender as Label;
            int index = int.Parse(clicked.Name.Split('l')[2]);
            pController.InsertValue(input, index);
        }

        public void EnableInput()
        {
            for (int i = 1; i <= 81; i++)
            {
                this.Controls.Find("label" + i.ToString(), false)[0].Click -= label_Click;
                this.Controls.Find("label" + i.ToString(), false)[0].Click += label_Click;
            }

            Clear.Enabled = true;
            PauseB.Enabled = true;
        }

        public void DisableInput()
        {
            for (int i = 1; i <= 81; i++)
            {
                this.Controls.Find("label" + i.ToString(), false)[0].Click -= label_Click;
            }

            Clear.Enabled = false;
            PauseB.Enabled = false;
            Game_Timer.Enabled = false;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            pController.ClearPuzzle();
        }

        private void Game_Timer_Tick(object sender, EventArgs e)
        {
            tControler.Tick();
        }

        private void Show_Stopper(Func<int> func)
        {
            Game_Timer.Stop();
            func();
            Game_Timer.Start();
        }

        private int New_Game()
        {
            if (pController.NewGame())
            {
                hController = new HintController(button10);
                tControler = new TimeController(Game_Time);
                Game_Timer.Enabled = true;
            }
            return 1;
        }

        private int Pause()
        {
            MessageBox.Show("Press OK to continue!");
            return 1;
        }

        private void PauseB_Click(object sender, EventArgs e)
        {
            Show_Stopper(Pause);
        }
    }
}
