using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetVolveGUI
{
    sealed class DoubleBufferedPanel : Panel
    {

        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }

    }
}
