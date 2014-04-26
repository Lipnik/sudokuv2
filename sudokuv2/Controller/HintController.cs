using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudokuv2.View;
using System.Windows.Forms;

namespace sudokuv2.Controller
{
    public class HintController
    {
        public HintController(Button hintB)
        {
            this.hintB = hintB;
            hintCount = 3;
            this.hintB.Text = "Hint(" + hintCount.ToString() + ")";
            this.hintB.Enabled = true;
        }

        public void HintUsed()
        {
            hintCount--;
            this.hintB.Text = "Hint(" + hintCount.ToString() + ")";
            if (hintCount==0)
            {
                this.hintB.Enabled = false;
            }
        }

        public void LoadHints(int hintCount)
        {
            hintCount = hintCount;
            this.hintB.Text = "Hint(" + hintCount.ToString() + ")";
            if (hintCount == 0)
            {
                this.hintB.Enabled = false;
            }
        }

        private Button hintB;
        public int hintCount { get; private set; }
    }
}
