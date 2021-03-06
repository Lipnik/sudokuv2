﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudokuv2.View
{
    public interface IView
    {
        object GetPresentation();

        object[] GiveControl();

        void TakeControl(object[] controllers);

        DialogResult ShowDialog();

        void HintUpdate(int value);

        void DisableInput();

        void EnableInput();
    }
}
