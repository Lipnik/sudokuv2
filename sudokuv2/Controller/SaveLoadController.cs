using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudokuv2.View;
using sudokuv2.Model;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace sudokuv2.Controller
{
    public class SaveLoadController
    {
        public SaveLoadController(IView form,SaveFileDialog sDialog,OpenFileDialog lDialog)
        {
            this.form = form;
            saveDialog = sDialog;
            loadDialog = lDialog;
            inGame = false;
        }

        public void  SetSaveDataControllers(PuzzleController Pcontroller, TimeController Tcontroller,HintController Hcontroller)
        {
            this.pController = Pcontroller;
            this.tController = Tcontroller;
            this.hController = Hcontroller;
        }

        public bool SaveBeforeExit()
        {
            if (inGame)
            {
                var answer = MessageBox.Show("Save before exit?", "Exit", MessageBoxButtons.YesNoCancel);
                if (answer == DialogResult.Yes)
                {
                    return Save();
                }
                else if (answer == DialogResult.No)
                {
                    return false;
                }
                return true; 
            }
            return false;
        }

        public bool Save()
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {              
                using (Stream stream= new FileStream(saveDialog.FileName,FileMode.Create,FileAccess.Write))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, new SaveGame(pController.puzzle,tController.time,hController.hCount));
                }
                return false;
            }
            return true;
        }

        public bool Load()
        {
            if (loadDialog.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    SaveGame game;
                    using (Stream stream = new FileStream(loadDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        game = (SaveGame)formatter.Deserialize(stream);
                    }

                    var controls = form.GiveControl();
                    pController = new PuzzleController((IView)controls[0]);
                    tController = new TimeController((Label)controls[1]);
                    hController = new HintController((Button)controls[2]);

                    pController.LoadPuzzle(game.puzzle);
                    tController.LoadTime(game.time);
                    hController.LoadHints(game.hintCount);

                    form.TakeControl(new object[] { pController, tController, hController });
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Selected file is not valid save data!");
                    return false;
                }
            }
            return false;
        }

        private SaveFileDialog saveDialog;
        private OpenFileDialog loadDialog;
        private PuzzleController pController;
        private TimeController tController;
        private HintController hController;
        private IView form;
        public bool inGame{private get;set;}
    }
}
