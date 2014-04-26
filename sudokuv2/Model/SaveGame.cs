using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.Model
{
    [Serializable]
    public class SaveGame
    {
        public SaveGame(Puzzle sPuzzle, int sTime, int sHintCount)
        {
            puzzle = sPuzzle;
            time = sTime;
            hintCount = sHintCount;
        }

        public Puzzle puzzle{get;set;}
        public int time{get;set;}
        public int hintCount{get;set;}
    }
}
