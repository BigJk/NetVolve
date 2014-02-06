using System.Windows.Forms;

namespace NetVolveGUI
{
    class DoubleBufferedListView : ListView
    {

        public DoubleBufferedListView()
        {
            DoubleBuffered = true;
        }

    }
}
