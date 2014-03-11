using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudokuv2.View;
using System.Windows.Forms;

namespace sudokuv2.Controller
{
    public class TimeController
    {
        public TimeController(Label gameTime)
        {
            this.gameTime = gameTime;
            time = 0;
            this.gameTime.Text = this.ToString();
        }

        public void Tick()
        {
            time++;
            gameTime.Text = this.ToString();
        }

        public override string ToString()
        {
            int min = time / 60;
            int sec = time % 60;
            if (min<10 && sec <10)
            {
                return "0"+min.ToString() + ":0" + sec.ToString();

            }
            else if (min<10)
            {
                return "0"+min.ToString() + ":" + sec.ToString();
            }
            else if (sec<10)
            {
                return min.ToString() + ":0" + sec.ToString();
            }
            else
            {
                return min.ToString() + ":" + sec.ToString();
            }
        }

        private Label gameTime;
        int time;//seconds
    }
}
