using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AgateLib;
using AgateLib.DisplayLib;
using AgateLib.Geometry;
using NetVolveLib.Mars;
using NetVolveLib.Redcode.Lines;

namespace NetVolveGUI
{
    static class Program
    {

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (AgateSetup setup = new AgateSetup())
            {

                setup.Initialize(true, false, false);
                if (setup.WasCanceled) return;

                mainFrm frm = new mainFrm();
                frm.Show();

                DisplayWindow window = DisplayWindow.CreateFromControl(frm.pictureBox1);

                while (!window.IsClosed)
                {
                    Display.BeginFrame();
                    Display.Clear(Color.White);

                    if (frm.grid != null)
                    {
                        int rec = frm.pictureBox1.Width / frm.grid.Size;
                        for (int y = 0; y < frm.grid.Size; y++)
                        {
                            for (int x = 0; x < frm.grid.Size; x++)
                            {
                                Display.FillRect(new Rectangle(x * rec, y * rec, rec, rec),
                                    Color.FromArgb(frm.grid.Cells[y, x].Owner.Color.R, frm.grid.Cells[y, x].Owner.Color.G,
                                        frm.grid.Cells[y, x].Owner.Color.B));
                            }
                        }
                    }

                    Display.EndFrame();
                    Core.KeepAlive();
                    Thread.Sleep(1);
                }

            }

            //Application.Run(new mainFrm());
        }
    }
}
