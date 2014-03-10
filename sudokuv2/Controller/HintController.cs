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
            hCount = 3;
            this.hintB.Text = "Hint(" + hCount.ToString() + ")";
            this.hintB.Enabled = true;
        }

        public void HintUsed()
        {
            hCount--;
            this.hintB.Text = "Hint(" + hCount.ToString() + ")";
            if (hCount==0)
            {
                this.hintB.Enabled = false;
            }
        }

        private Button hintB;
        private int hCount;
    }
}
